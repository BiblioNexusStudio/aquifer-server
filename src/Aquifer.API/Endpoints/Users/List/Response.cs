using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Users.List;

public class Response
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required UserRole Role { get; set; }
    public required string? CompanyName { get; set; }
    public bool IsEmailVerified { get; set; }
}