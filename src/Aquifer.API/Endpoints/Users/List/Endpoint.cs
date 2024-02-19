using Aquifer.API.Common;
using Aquifer.API.Services;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;

namespace Aquifer.API.Endpoints.Users.List;

public class Endpoint(AquiferDbContext dbContext, IUserService userService) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/users", "/admin/users");
        Permissions(PermissionName.ReadUsers);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var self = await userService.GetUserFromJwtAsync(ct);
        if (self.Role is not UserRole.Admin and not UserRole.Publisher and not UserRole.Manager)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var users = dbContext.Users.Where(x => (self.Role == UserRole.Manager && x.CompanyId == self.CompanyId) ||
                                               self.Role == UserRole.Publisher ||
                                               self.Role == UserRole.Admin).Select(user => new Response
                                               {
                                                   Id = user.Id,
                                                   Name = $"{user.FirstName} {user.LastName}",
                                                   Role = user.Role,
                                                   CompanyName = user.Company.Name,
                                                   Company = new CompanyResponse { Id = user.CompanyId, Name = user.Company.Name },
                                                   Email = user.Email,
                                                   IsEmailVerified = user.EmailVerified
                                               }).AsEnumerable().OrderBy(u => u.Name).ToList();

        await SendOkAsync(users, ct);
    }
}