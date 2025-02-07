using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Users.Role.Set;

public sealed class Request
{
    public required int UserId { get; init; }
    public UserRole? Role { get; set; }
}