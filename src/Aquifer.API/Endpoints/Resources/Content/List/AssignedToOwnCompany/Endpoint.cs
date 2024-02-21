using Aquifer.API.Services;
using Aquifer.Common.Extensions;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.List.AssignedToOwnCompany;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/assigned-to-own-company");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var resourceContents = (await dbContext.ResourceContentVersions
                .Where(rcv =>
                    rcv.AssignedUser != null &&
                    rcv.AssignedUser.CompanyId == user.CompanyId &&
                    (rcv.ResourceContent.Status == ResourceContentStatus.AquiferizeInProgress ||
                     rcv.ResourceContent.Status == ResourceContentStatus.TranslationInProgress))
                .Select(x => new Response
                {
                    ContentId = x.ResourceContentId,
                    EnglishLabel = x.ResourceContent.Resource.EnglishLabel,
                    ParentResourceName = x.ResourceContent.Resource.ParentResource.DisplayName,
                    Project = x.ResourceContent.Projects.First(),
                    AssignedUser = new UserResponse { User = x.AssignedUser! },
                    LanguageEnglishDisplay = x.ResourceContent.Language.EnglishDisplay,
                    WordCount = x.WordCount,
                    Status = x.ResourceContent.Status.GetDisplayName()
                }).ToListAsync(ct))
            .OrderBy(x => x.DaysUntilDeadline).ThenBy(x => x.EnglishLabel).ToList();

        await SendOkAsync(resourceContents, ct);
    }
}