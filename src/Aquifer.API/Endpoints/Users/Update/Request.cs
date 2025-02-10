using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Users.Update;

public sealed class Request
{
    public required int UserId { get; init; }
    public required UserRole? Role { get; set; }
}