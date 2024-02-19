using Aquifer.API.Common;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Users.List;

public class Endpoint(AquiferDbContext dbContext) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/users", "/admin/users");
        Permissions(PermissionName.ReadUsers);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = await dbContext.Users.Select(user => new Response
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