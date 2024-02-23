using Aquifer.API.Services;
using Aquifer.Common.Extensions;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToSelf.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/assigned-to-self");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var resourceContents = (await dbContext.ResourceContentVersions
                .Where(rcv =>
                    rcv.AssignedUserId == user.Id &&
                    (rcv.ResourceContent.Status == ResourceContentStatus.AquiferizeInProgress ||
                     rcv.ResourceContent.Status == ResourceContentStatus.AquiferizeInReview ||
                     rcv.ResourceContent.Status == ResourceContentStatus.TranslationInProgress ||
                     rcv.ResourceContent.Status == ResourceContentStatus.TranslationInReview))
                .Select(x => new Response
                {
                    Id = x.ResourceContentId,
                    EnglishLabel = x.ResourceContent.Resource.EnglishLabel,
                    ParentResourceName = x.ResourceContent.Resource.ParentResource.DisplayName,
                    LanguageEnglishDisplay = x.ResourceContent.Language.EnglishDisplay,
                    ProjectEntity = x.ResourceContent.Projects.FirstOrDefault(),
                    HistoryCreated =
                        x.ResourceContentVersionAssignedUserHistories.Where(auh => auh.AssignedUserId == user.Id)
                            .Max(auh => auh.Created),
                    WordCount = x.WordCount,
                    Status = x.ResourceContent.Status.GetDisplayName()
                }).ToListAsync(ct))
            .OrderByDescending(x => x.DaysSinceAssignment).ThenBy(x => x.EnglishLabel).ToList();

        await SendOkAsync(resourceContents, ct);
    }
}