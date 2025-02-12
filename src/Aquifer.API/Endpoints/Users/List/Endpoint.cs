using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Users.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<IReadOnlyList<Response>>
{
    public override void Configure()
    {
        Get("/users");
        Permissions(PermissionName.ReadUsers);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var self = await userService.GetUserFromJwtAsync(ct);

        var users = await dbContext.Users.Where(x =>
                (userService.HasPermission(PermissionName.ReadAllUsers) ||
                 (userService.HasPermission(PermissionName.ReadUsers) && self.CompanyId == x.CompanyId)) && x.Enabled)
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName).Select(user => new Response
            {
                Id = user.Id,
                Name = $"{user.FirstName} {user.LastName}",
                Role = user.Role,
                CompanyName = user.Company.Name,
                Company = new CompanyResponse { Id = user.CompanyId, Name = user.Company.Name },
                Email = user.Email,
                IsEmailVerified = user.EmailVerified
            }).ToListAsync(ct);

        await SendOkAsync(users, ct);
    }
}