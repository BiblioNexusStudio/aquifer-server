using Aquifer.API.Common;
using Aquifer.API.Modules.AdminResources;
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
        var language = await dbContext.Languages.FindAsync([request.LanguageId], ct) ??
                       AddEntityNotFoundError<LanguageEntity>(r => r.LanguageId);
        var projectManagerUser = await dbContext.Users.FindAsync([request.ProjectManagerUserId], ct) ??
                                 AddEntityNotFoundError<UserEntity>(r => r.ProjectManagerUserId);
        var company = await dbContext.Companies.FindAsync([request.CompanyId], ct) ??
                      AddEntityNotFoundError<CompanyEntity>(r => r.CompanyId);
        var projectPlatform = await dbContext.ProjectPlatforms.FindAsync([request.ProjectPlatformId], ct) ??
                              AddEntityNotFoundError<ProjectPlatformEntity>(r => r.ProjectPlatformId);

        UserEntity? companyLeadUser = null;

        if (projectPlatform?.Name == "Aquifer")
        {
            companyLeadUser = await dbContext.Users.FindAsync([request.CompanyLeadUserId], ct) ??
                              AddEntityNotFoundError<UserEntity>(r => r.CompanyLeadUserId);

            if (companyLeadUser?.Role is not UserRole.Publisher or UserRole.Manager)
            {
                AddError(r => r.CompanyLeadUserId, "Company lead must be a Manager or Publisher.");
            }
        }

        if (projectManagerUser?.Role != UserRole.Publisher)
        {
            AddError(r => r.ProjectManagerUserId, "Project manager must be a Publisher.");
        }

        var resourceContents = await CreateOrFindResourceContentFromResourceIds(language, request, user, ct);

        ThrowIfAnyErrors();

        var wordCount = resourceContents.Aggregate(0, (total, rc) => (rc.Versions.FirstOrDefault(v => v.IsDraft)?.WordCount ?? 0) + total);

        var newProject = new ProjectEntity
        {
            Name = request.Title,
            SourceWordCount = wordCount,
            ResourceContents = resourceContents,
            Language = language!,
            ProjectManagerUser = projectManagerUser!,
            CompanyLeadUser = companyLeadUser,
            Company = company!,
            ProjectPlatform = projectPlatform!
        };

        await dbContext.Projects.AddAsync(newProject, ct);

        await dbContext.SaveChangesAsync(ct);

        await SendAsync(new Response { Id = newProject.Id }, 201, ct);
    }

    private async Task<List<ResourceContentEntity>> CreateOrFindResourceContentFromResourceIds(LanguageEntity? language, Request request,
        UserEntity user,
        CancellationToken ct)
    {
        if (language?.ISO6393Code == "eng")
        {
            return await CreateOrFindAquiferizationResourceContent(language, request, ct);
        }

        if (language is not null)
        {
            return await CreateOrFindTranslationResourceContent(language, request, user, ct);
        }

        return [];
    }

    private async Task<List<ResourceContentEntity>> CreateOrFindAquiferizationResourceContent(LanguageEntity language, Request request,
        CancellationToken ct)
    {
        var resourceContents = await dbContext.ResourceContents
            .Where(rc => request.ResourceIds.Contains(rc.ResourceId) && rc.LanguageId == language.Id)
            .Include(rc => rc.Versions.OrderByDescending(v => v.Created)).Include(rc => rc.Projects).ToListAsync(ct);

        if (resourceContents.Count < request.ResourceIds.Length)
        {
            AddError(r => r.ResourceIds, "One or more not found.");
            return [];
        }

        foreach (var resourceContent in resourceContents)
        {
            if (resourceContent.Status != ResourceContentStatus.New)
            {
                AddError(r => r.ResourceIds, "All resources must be in the New status.");
                return [];
            }

            if (resourceContent.Projects.Count > 0)
            {
                AddError(r => r.ResourceIds, "One or more resources are already in a project.");
                return [];
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
                    AddError(r => r.ResourceIds, "One or more resources are missing a published English version to use as a base.");
                    return [];
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
                    AddError(r => r.ResourceIds, "One or more resources are already in a project.");
                    return [];
                }

                if (resourceContent.Status != ResourceContentStatus.TranslationNotStarted)
                {
                    AddError(r => r.ResourceIds, "One or more resources exist but are not in TranslationNotStarted status.");
                    return [];
                }

                var existingVersion = resourceContent.Versions.FirstOrDefault(v => v.IsDraft);
                if (existingVersion is null)
                {
                    AddError(r => r.ResourceIds, "One or more resources exist but are missing a draft to use.");
                    return [];
                }

                resourceContents.Add(resourceContent);
            }
        }

        return resourceContents;
    }
}