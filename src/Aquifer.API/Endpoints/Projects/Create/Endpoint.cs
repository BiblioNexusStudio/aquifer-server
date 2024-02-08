using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Projects.Create;

public class Endpoint(AquiferDbContext dbContext, IAdminResourceHistoryService historyService, IUserService userService)
    : BaseEndpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/projects");
        Permissions(PermissionName.CreateProject);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);

        var language = await dbContext.Languages.SingleOrDefaultAsync(l => l.Id == request.LanguageId, ct);
        if (language is null)
        {
            ThrowEntityNotFoundError(r => r.LanguageId);
        }

        var projectManagerUser = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.ProjectManagerUserId, ct);
        if (projectManagerUser is null)
        {
            ThrowEntityNotFoundError(r => r.ProjectManagerUserId);
        }
        if (projectManagerUser.Role != UserRole.Publisher)
        {
            ThrowError(r => r.ProjectManagerUserId, "Project manager must be a Publisher.");
        }

        var company = await dbContext.Companies.SingleOrDefaultAsync(c => c.Id == request.CompanyId, ct);
        if (company is null)
        {
            ThrowEntityNotFoundError(r => r.CompanyId);
        }

        var projectPlatform = await dbContext.ProjectPlatforms.SingleOrDefaultAsync(p => p.Id == request.ProjectPlatformId, ct);
        if (projectPlatform is null)
        {
            ThrowEntityNotFoundError(r => r.ProjectPlatformId);
        }

        var companyLeadUser = await MaybeGetCompanyLeadUser(projectPlatform, request, ct);

        var resourceContents = await CreateOrFindResourceContentFromResourceIds(language, request, user, ct);

        var wordCount = resourceContents.Sum(rc => rc.Versions.FirstOrDefault(v => v.IsDraft)?.WordCount ?? 0);

        var newProject = new ProjectEntity
        {
            Name = request.Title,
            SourceWordCount = wordCount,
            ResourceContents = resourceContents,
            Language = language,
            ProjectManagerUser = projectManagerUser,
            CompanyLeadUser = companyLeadUser,
            Company = company,
            ProjectPlatform = projectPlatform
        };

        await dbContext.Projects.AddAsync(newProject, ct);

        await dbContext.SaveChangesAsync(ct);

        await SendAsync(new Response { Id = newProject.Id }, 201, ct);
    }

    private async Task<UserEntity?> MaybeGetCompanyLeadUser(ProjectPlatformEntity projectPlatform, Request request, CancellationToken ct)
    {
        if (projectPlatform?.Name == "Aquifer")
        {
            var companyLeadUser = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.CompanyLeadUserId, ct);
            if (companyLeadUser is null)
            {
                ThrowEntityNotFoundError(r => r.CompanyLeadUserId);
            }

            if (companyLeadUser.Role is not (UserRole.Publisher or UserRole.Manager))
            {
                ThrowError(r => r.CompanyLeadUserId, "Company lead must be a Manager or Publisher.");
            }
        }

        return null;
    }

    private async Task<List<ResourceContentEntity>> CreateOrFindResourceContentFromResourceIds(LanguageEntity language, Request request,
        UserEntity user,
        CancellationToken ct)
    {
        if (language.ISO6393Code == "eng")
        {
            return await CreateOrFindAquiferizationResourceContent(language, request, ct);
        }

        return await CreateOrFindTranslationResourceContent(language, request, user, ct);
    }

    private async Task<List<ResourceContentEntity>> CreateOrFindAquiferizationResourceContent(LanguageEntity language, Request request,
        CancellationToken ct)
    {
        var resourceContents = await dbContext.ResourceContents
            .Where(rc => request.ResourceIds.Contains(rc.ResourceId) && rc.LanguageId == language.Id)
            .Include(rc => rc.Versions.OrderByDescending(v => v.Created)).Include(rc => rc.Projects).ToListAsync(ct);

        if (resourceContents.Count < request.ResourceIds.Length)
        {
            ThrowError(r => r.ResourceIds, "One or more not found.");
        }

        foreach (var resourceContent in resourceContents)
        {
            if (resourceContent.Status != ResourceContentStatus.New)
            {
                ThrowError(r => r.ResourceIds, "All resources must be in the New status.");
            }

            if (resourceContent.Projects.Count > 0)
            {
                ThrowError(r => r.ResourceIds, "One or more resources are already in a project.");
            }

            var draftVersion = resourceContent.Versions.FirstOrDefault(v => v.IsDraft);
            if (draftVersion is null)
            {
                var firstVersion = resourceContent.Versions.First();
                resourceContent.Versions.Add(new ResourceContentVersionEntity
                {
                    IsPublished = false,
                    IsDraft = true,
                    DisplayName = firstVersion.DisplayName,
                    Content = firstVersion.Content,
                    ContentSize = firstVersion.ContentSize,
                    WordCount = firstVersion.WordCount,
                    Version = firstVersion.Version + 1,
                    ResourceContent = resourceContent
                });
            }
        }

        return resourceContents;
    }

    private async Task<List<ResourceContentEntity>> CreateOrFindTranslationResourceContent(LanguageEntity language, Request request,
        UserEntity user, CancellationToken ct)
    {
        var englishOrLanguageResourceContents = await dbContext.ResourceContents
            .Where(rc => request.ResourceIds.Contains(rc.ResourceId) && (rc.LanguageId == language.Id ||
                                                                         (rc.Resource.ResourceContents.All(rci =>
                                                                              rci.LanguageId != language.Id) &&
                                                                          rc.Language.ISO6393Code == "eng")))
            .Include(rc => rc.Versions).Include(rc => rc.Projects).Include(rc => rc.Language).ToListAsync(ct);

        List<ResourceContentEntity> resourceContents = [];

        foreach (var resourceContent in englishOrLanguageResourceContents)
        {
            if (resourceContent.Language.ISO6393Code == "eng")
            {
                var baseVersion = resourceContent.Versions.FirstOrDefault(v => v.IsPublished);
                if (baseVersion is null)
                {
                    ThrowError(r => r.ResourceIds, "One or more resources are missing a published English version to use as a base.");
                }

                var newResourceContentVersion = new ResourceContentVersionEntity
                {
                    IsPublished = false,
                    IsDraft = true,
                    DisplayName = baseVersion.DisplayName,
                    Content = baseVersion.Content,
                    ContentSize = baseVersion.ContentSize,
                    WordCount = baseVersion.WordCount,
                    Version = 1
                };
                var newResourceContent = new ResourceContentEntity
                {
                    LanguageId = language.Id,
                    ResourceId = resourceContent.ResourceId,
                    MediaType = resourceContent.MediaType,
                    Status = ResourceContentStatus.TranslationNotStarted,
                    Trusted = true,
                    Versions = [newResourceContentVersion]
                };
                await dbContext.ResourceContents.AddAsync(newResourceContent, ct);
                await historyService.AddStatusHistoryAsync(newResourceContentVersion, ResourceContentStatus.TranslationNotStarted, user.Id,
                    ct);
                resourceContents.Add(newResourceContent);
            }
            else
            {
                if (resourceContent.Projects.Count > 0)
                {
                    ThrowError(r => r.ResourceIds, "One or more resources are already in a project.");
                }

                if (resourceContent.Status != ResourceContentStatus.TranslationNotStarted)
                {
                    ThrowError(r => r.ResourceIds, "One or more resources exist but are not in TranslationNotStarted status.");
                }

                var existingVersion = resourceContent.Versions.FirstOrDefault(v => v.IsDraft);
                if (existingVersion is null)
                {
                    ThrowError(r => r.ResourceIds, "One or more resources exist but are missing a draft to use.");
                }

                resourceContents.Add(resourceContent);
            }
        }

        return resourceContents;
    }
}