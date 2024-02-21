using Aquifer.API.Common.Dtos;
using Aquifer.API.Services;
using Aquifer.Common.Extensions;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToOwnCompany.List;

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
                .Where(rcv => rcv.AssignedUser != null && rcv.AssignedUser.CompanyId == user.CompanyId)
                .Select(x => new Response
                {
                    Id = x.ResourceContentId,
                    EnglishLabel = x.ResourceContent.Resource.EnglishLabel,
                    ParentResourceName = x.ResourceContent.Resource.ParentResource.DisplayName,
                    ProjectName = x.ResourceContent.Projects.First() == null ? null : x.ResourceContent.Projects.First()!.Name,
                    ProjectProjectedDeliveryDate =
                        x.ResourceContent.Projects.First() == null ? null : x.ResourceContent.Projects.First()!.ProjectedDeliveryDate,
                    AssignedUser = UserDto.FromUserEntity(x.AssignedUser)!,
                    LanguageEnglishDisplay = x.ResourceContent.Language.EnglishDisplay,
                    WordCount = x.WordCount,
                    Status = x.ResourceContent.Status.GetDisplayName()
                }).ToListAsync(ct))
            .OrderBy(x => x.DaysUntilProjectDeadline).ThenBy(x => x.EnglishLabel).ToList();

        await SendOkAsync(resourceContents, ct);
    }
}