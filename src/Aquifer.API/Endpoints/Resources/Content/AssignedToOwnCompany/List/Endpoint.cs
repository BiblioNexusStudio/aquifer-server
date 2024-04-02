using Aquifer.API.Common;
using Aquifer.API.Common.Dtos;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Resources.Content.AssignedToOwnCompany.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/resources/content/assigned-to-own-company");
        Permissions(PermissionName.ReadCompanyContentAssignments);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await userService.GetUserFromJwtAsync(ct);
        var resourceContents = (await dbContext.ResourceContentVersions
                .Where(rcv => rcv.IsDraft && rcv.AssignedUser != null && rcv.AssignedUser.CompanyId == user.CompanyId)
                .Where(rcv => rcv.ResourceContent.Status == ResourceContentStatus.AquiferizeInProgress ||
                              rcv.ResourceContent.Status == ResourceContentStatus.TranslationInProgress)
                .Select(x => new Response
                {
                    Id = x.ResourceContentId,
                    EnglishLabel = x.ResourceContent.Resource.EnglishLabel,
                    ParentResourceName = x.ResourceContent.Resource.ParentResource.DisplayName,
                    ProjectEntity = x.ResourceContent.Projects.FirstOrDefault(),
                    AssignedUser = UserDto.FromUserEntity(x.AssignedUser)!,
                    LanguageEnglishDisplay = x.ResourceContent.Language.EnglishDisplay,
                    WordCount = x.WordCount
                }).ToListAsync(ct))
            .OrderBy(x => x.DaysUntilProjectDeadline).ThenBy(x => x.EnglishLabel).ToList();

        await SendOkAsync(resourceContents, ct);
    }
}