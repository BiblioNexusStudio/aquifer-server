using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using static Aquifer.API.Helpers.EndpointHelpers;

namespace Aquifer.API.Endpoints.Projects.Create;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/projects");
        Permissions(PermissionName.CreateProject);
    }

    public override async Task HandleAsync(Request request, CancellationToken ct)
    {
        var newProject = await BuildProjectFromRequest(request, ct);

        await dbContext.Projects.AddAsync(newProject, ct);

        await dbContext.SaveChangesAsync(ct);

        await SendOkAsync(new Response { Id = newProject.Id }, ct);
    }

    private async Task<ProjectEntity> BuildProjectFromRequest(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);

        var language = await dbContext.Languages.AsTracking().SingleOrDefaultAsync(l => l.Id == request.LanguageId, ct);
        if (language is null)
        {
            ThrowEntityNotFoundError<Request>(r => r.LanguageId);
        }

        var projectManagerUser = await dbContext.Users.AsTracking()
            .SingleOrDefaultAsync(u => u.Id == request.ProjectManagerUserId && u.Enabled, ct);
        if (projectManagerUser is null)
        {
            ThrowEntityNotFoundError<Request>(r => r.ProjectManagerUserId);
        }

        if (projectManagerUser.Role != UserRole.Publisher)
        {
            ThrowError(r => r.ProjectManagerUserId, "Project manager must be a Publisher.");
        }

        var company = await dbContext.Companies.AsTracking().SingleOrDefaultAsync(c => c.Id == request.CompanyId, ct);
        if (company is null)
        {
            ThrowEntityNotFoundError<Request>(r => r.CompanyId);
        }

        var projectPlatform =
            await dbContext.ProjectPlatforms.AsTracking().SingleOrDefaultAsync(p => p.Id == request.ProjectPlatformId, ct);
        if (projectPlatform is null)
        {
            ThrowEntityNotFoundError<Request>(r => r.ProjectPlatformId);
        }

        if (dbContext.Projects.AsTracking().Any(p => p.Name == request.Title))
        {
            ThrowError(r => r.Title, "A project with this title already exists.");
        }

        var companyLeadUser = await MaybeGetCompanyLeadUser(projectPlatform, request, ct);

        var resourceContents = await CreateOrFindResourceContentFromResourceIds(language.Id, request, user, ct);

        var wordCount = await dbContext.ResourceContentVersions
            .AsTracking()
            .Where(rcv => request.ResourceIds.Contains(rcv.ResourceContent.ResourceId) &&
                          rcv.IsPublished &&
                          rcv.ResourceContent.LanguageId == 1)
            .Select(rcv => rcv.WordCount ?? 0)
            .SumAsync(ct);

        return new ProjectEntity
        {
            Name = request.Title,
            SourceWordCount = wordCount,
            ProjectResourceContents = resourceContents
                .Select(rc => new ProjectResourceContentEntity { ResourceContent = rc })
                .ToList(),
            Language = language,
            ProjectManagerUser = projectManagerUser,
            CompanyLeadUser = companyLeadUser,
            Company = company,
            ProjectPlatform = projectPlatform
        };
    }

    private async Task<UserEntity?> MaybeGetCompanyLeadUser(ProjectPlatformEntity projectPlatform, Request request, CancellationToken ct)
    {
        if (projectPlatform?.Name == "Aquifer")
        {
            var companyLeadUser = await dbContext.Users
                .AsTracking()
                .SingleOrDefaultAsync(u => u.Id == request.CompanyLeadUserId && u.Enabled, ct);
            if (companyLeadUser is null)
            {
                ThrowEntityNotFoundError<Request>(r => r.CompanyLeadUserId);
            }

            if (companyLeadUser.Role is not (UserRole.Publisher or UserRole.Manager))
            {
                ThrowError(r => r.CompanyLeadUserId, "Company lead must be a Manager or Publisher.");
            }

            return companyLeadUser;
        }

        return null;
    }

    private async Task<List<ResourceContentEntity>> CreateOrFindResourceContentFromResourceIds(int languageId,
        Request request,
        UserEntity user,
        CancellationToken ct)
    {
        if (languageId == Constants.EnglishLanguageId)
        {
            return await CreateOrFindAquiferizationResourceContent(languageId, request, ct);
        }

        return await CreateOrFindTranslationResourceContent(languageId, request, user, ct);
    }

    private async Task<List<ResourceContentEntity>> CreateOrFindAquiferizationResourceContent(int languageId,
        Request request,
        CancellationToken ct)
    {
        var resourceContents = await dbContext.ResourceContents
            .AsTracking()
            .Where(rc => request.ResourceIds.Contains(rc.ResourceId) &&
                         rc.LanguageId == languageId &&
                         rc.MediaType != ResourceContentMediaType.Audio)
            .Include(rc => rc.Versions.OrderByDescending(v => v.Created))
            .Include(rc => rc.ProjectResourceContents)
            .ToListAsync(ct);

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

            if (resourceContent.ProjectResourceContents.Count > 0)
            {
                ThrowError(r => r.ResourceIds, "One or more resources are already in a project.");
            }

            resourceContent.Status = ResourceContentStatus.AquiferizeAwaitingAiDraft;

            var draftVersion = resourceContent.Versions.FirstOrDefault(v => v.IsDraft);
            if (draftVersion is null)
            {
                var firstVersion = resourceContent.Versions.First();
                resourceContent.Versions.Add(new ResourceContentVersionEntity
                {
                    IsPublished = false,
                    IsDraft = true,
                    ReviewLevel = ResourceContentVersionReviewLevel.Professional,
                    DisplayName = firstVersion.DisplayName,
                    Content = firstVersion.Content,
                    ContentSize = firstVersion.ContentSize,
                    WordCount = firstVersion.WordCount,
                    SourceWordCount = firstVersion.WordCount,
                    Version = firstVersion.Version + 1,
                    ResourceContent = resourceContent
                });
            }
        }

        return resourceContents;
    }

    private async Task<List<ResourceContentEntity>> CreateOrFindTranslationResourceContent(int languageId,
        Request request,
        UserEntity user,
        CancellationToken ct)
    {
        var englishOrLanguageResourceContents = await dbContext.ResourceContents
            .AsTracking()
            .Where(rc => request.ResourceIds.Contains(rc.ResourceId) && rc.MediaType != ResourceContentMediaType.Audio)
            .Where(rc => rc.LanguageId == languageId ||
                         (rc.Resource.ResourceContents.All(rci => rci.LanguageId != languageId) &&
                          rc.LanguageId == Constants.EnglishLanguageId))
            .Include(rc => rc.Versions)
            .Include(rc => rc.ProjectResourceContents)
            .Include(rc => rc.Language)
            .ToListAsync(ct);

        List<ResourceContentEntity> resourceContents = [];

        foreach (var resourceContent in englishOrLanguageResourceContents)
        {
            if (resourceContent.LanguageId == Constants.EnglishLanguageId)
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
                    ReviewLevel = ResourceContentVersionReviewLevel.Professional,
                    DisplayName = baseVersion.DisplayName,
                    Content = baseVersion.Content,
                    ContentSize = baseVersion.ContentSize,
                    WordCount = baseVersion.WordCount,
                    SourceWordCount = baseVersion.WordCount,
                    ResourceContentVersionStatusHistories =
                    [
                        new ResourceContentVersionStatusHistoryEntity
                        {
                            Status = ResourceContentStatus.TranslationAwaitingAiDraft,
                            ChangedByUserId = user.Id,
                            Created = DateTime.UtcNow
                        }
                    ],
                    Version = 1
                };
                var newResourceContent = new ResourceContentEntity
                {
                    LanguageId = languageId,
                    ResourceId = resourceContent.ResourceId,
                    MediaType = resourceContent.MediaType,
                    Status = ResourceContentStatus.TranslationAwaitingAiDraft,
                    Trusted = true,
                    Versions = [newResourceContentVersion]
                };
                await dbContext.ResourceContents.AddAsync(newResourceContent, ct);
                resourceContents.Add(newResourceContent);
            }
            else
            {
                if (resourceContent.ProjectResourceContents.Count > 0)
                {
                    ThrowError(r => r.ResourceIds, "One or more resources are already in a project.");
                }

                if (resourceContent.Status != ResourceContentStatus.TranslationAwaitingAiDraft)
                {
                    ThrowError(r => r.ResourceIds, "One or more resources exist but are not in TranslationAwaitingAiDraft status.");
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