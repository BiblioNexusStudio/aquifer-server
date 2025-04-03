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
        var newProject = await BuildProjectFromRequestAsync(request, ct);

        await dbContext.Projects.AddAsync(newProject, ct);

        await dbContext.SaveChangesAsync(ct);

        await SendOkAsync(new Response { Id = newProject.Id }, ct);
    }

    private async Task<ProjectEntity> BuildProjectFromRequestAsync(Request request, CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);

        var languages = await dbContext.Languages
            .AsTracking()
            .Where(l =>
                l.Id == request.SourceLanguageId ||
                (request.TargetLanguageId.HasValue && l.Id == request.TargetLanguageId))
            .ToListAsync(ct);

        var sourceLanguage = languages.SingleOrDefault(l => l.Id == request.SourceLanguageId);
        if (sourceLanguage is null)
        {
            ThrowEntityNotFoundError<Request>(r => r.SourceLanguageId);
        }

        var targetLanguage = languages.SingleOrDefault(l => l.Id == request.TargetLanguageId);
        if (targetLanguage is null && !request.IsAlreadyTranslated)
        {
            ThrowEntityNotFoundError<Request>(r => r.TargetLanguageId);
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

        if (dbContext.Projects.AsTracking().Any(p => p.Name == request.Title))
        {
            ThrowError(r => r.Title, "A project with this title already exists.");
        }

        var companyLeadUser = await GetCompanyLeadUserAsync(request, ct);
        var resourceContents = await CreateOrFindResourceContentFromResourceIdsAsync(request, user, ct);

        var wordCount = await dbContext.ResourceContentVersions.AsTracking()
            .Where(rcv => request.ResourceIds.Contains(rcv.ResourceContent.ResourceId) &&
                rcv.IsPublished &&
                rcv.ResourceContent.LanguageId == 1)
            .Select(rcv => rcv.WordCount ?? 0)
            .SumAsync(ct);

        return new ProjectEntity
        {
            Name = request.Title,
            SourceWordCount = wordCount,
            ProjectResourceContents = resourceContents.Select(rc => new ProjectResourceContentEntity { ResourceContent = rc }).ToList(),
            Language = request.IsAlreadyTranslated ? sourceLanguage : targetLanguage!,
            ProjectManagerUser = projectManagerUser,
            CompanyLeadUser = companyLeadUser,
            Company = company,
            ProjectPlatformId = 1
        };
    }

    private async Task<UserEntity?> GetCompanyLeadUserAsync(Request request, CancellationToken ct)
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

    private async Task<List<ResourceContentEntity>> CreateOrFindResourceContentFromResourceIdsAsync(
        Request request,
        UserEntity user,
        CancellationToken ct)
    {
        // Aquiferization
        if (request.SourceLanguageId == request.TargetLanguageId)
        {
            if (request.TargetLanguageId.Value != Constants.EnglishLanguageId)
            {
                ThrowError(r => r.TargetLanguageId, "Only English is currently supported for Aquiferization.");
            }

            return await CreateOrFindAquiferizationResourceContentAsync(request.TargetLanguageId.Value, request.ResourceIds, ct);
        }

        if (request.IsAlreadyTranslated)
        {
            if (request.SourceLanguageId == Constants.EnglishLanguageId)
            {
                ThrowError(
                    r => r.SourceLanguageId,
                    "English projects cannot be created with already translated resource contents.");
            }

            return await CreateOrFindTranslatedResourceContentAsync(request.SourceLanguageId, request.ResourceIds, user, ct);
        }

        return await CreateOrFindTranslationResourceContentAsync(request.SourceLanguageId, request.TargetLanguageId!.Value, request.ResourceIds, user, ct);
    }

    private async Task<List<ResourceContentEntity>> CreateOrFindAquiferizationResourceContentAsync(
        int languageId,
        IReadOnlyList<int> resourceIds,
        CancellationToken ct)
    {
        var resourceContents = await dbContext.ResourceContents.AsTracking()
            .Where(rc => resourceIds.Contains(rc.ResourceId) &&
                rc.LanguageId == languageId &&
                rc.MediaType != ResourceContentMediaType.Audio)
            .Include(rc => rc.Versions.OrderByDescending(v => v.Created))
            .Include(rc => rc.ProjectResourceContents)
            .ToListAsync(ct);

        if (resourceContents.Count < resourceIds.Count)
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

    private async Task<List<ResourceContentEntity>> CreateOrFindTranslatedResourceContentAsync(
        int languageId,
        IReadOnlyList<int> resourceIds,
        UserEntity user,
        CancellationToken ct)
    {
        var languageResourceContents = await dbContext.ResourceContents.AsTracking()
            .Where(rc => resourceIds.Contains(rc.ResourceId) &&
                rc.MediaType != ResourceContentMediaType.Audio &&
                rc.LanguageId == languageId)
            .Include(rc => rc.Versions)
            .Include(rc => rc.ProjectResourceContents)
            .Include(rc => rc.Language)
            .ToListAsync(ct);

        List<ResourceContentEntity> resourceContents = [];

        foreach (var resourceContent in languageResourceContents)
        {
            if (resourceContent.ProjectResourceContents.Count > 0)
            {
                ThrowError(r => r.ResourceIds, "One or more resources are already in a project.");
            }

            if (resourceContent.Versions.Any(rcv => rcv.IsDraft))
            {
                ThrowError(r => r.ResourceIds, "One or more resources already have a draft version.");
            }

            // this published base version should also be the most recent version because there are no draft versions
            var baseVersion = resourceContent.Versions.FirstOrDefault(v => v.IsPublished);
            if (baseVersion is null)
            {
                ThrowError(r => r.ResourceIds, "One or more resources are missing a published version to use as a base.");
            }

            const ResourceContentStatus status = ResourceContentStatus.New;

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
                        Status = status,
                        ChangedByUserId = user.Id,
                        Created = DateTime.UtcNow
                    }
                ],
                Version = baseVersion.Version + 1,
            };

            resourceContent.Versions.Add(newResourceContentVersion);
            resourceContent.Status = status;

            resourceContents.Add(resourceContent);
        }

        return resourceContents;
    }

    private async Task<List<ResourceContentEntity>> CreateOrFindTranslationResourceContentAsync(
        int sourceLanguageId,
        int targetLanguageId,
        IReadOnlyList<int> resourceIds,
        UserEntity user,
        CancellationToken ct)
    {
        var sourceLanguageOrTargetLanguageResourceContents = await dbContext.ResourceContents.AsTracking()
            .Where(rc => resourceIds.Contains(rc.ResourceId) && rc.MediaType != ResourceContentMediaType.Audio)
            .Where(rc => rc.LanguageId == targetLanguageId ||
                (rc.Resource.ResourceContents.All(rci => rci.LanguageId != targetLanguageId) && rc.LanguageId == sourceLanguageId))
            .Include(rc => rc.Versions)
            .Include(rc => rc.ProjectResourceContents)
            .Include(rc => rc.Language)
            .ToListAsync(ct);

        List<ResourceContentEntity> resourceContents = [];

        foreach (var resourceContent in sourceLanguageOrTargetLanguageResourceContents)
        {
            if (resourceContent.LanguageId == sourceLanguageId)
            {
                var baseVersion = resourceContent.Versions.FirstOrDefault(v => v.IsPublished);
                if (baseVersion is null)
                {
                    ThrowError(
                        r => r.ResourceIds,
                        "One or more resources are missing a published version to use as a base.");
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
                    LanguageId = targetLanguageId,
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
                    ThrowError(
                        r => r.ResourceIds,
                        "One or more resources exist but are not in TranslationAwaitingAiDraft status.");
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