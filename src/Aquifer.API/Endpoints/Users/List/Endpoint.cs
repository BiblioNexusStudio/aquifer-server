using System.Security.Claims;
using Aquifer.API.Common;
using Aquifer.Data;
using Aquifer.Data.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Users.List;

public class Endpoint(AquiferDbContext dbContext, IHttpContextAccessor _httpContextAccessor) : EndpointWithoutRequest<List<Response>>
{
    public override void Configure()
    {
        Get("/users", "/admin/users");
        Permissions(PermissionName.ReadUsers);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var providerId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var self = await dbContext.Users.Where(u => u.ProviderId == providerId).Include(u => u.Company).SingleAsync(ct);
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
        }).AsEnumerable().OrderBy(u => u.Name);

        await SendOkAsync([..users], ct);
    }
}