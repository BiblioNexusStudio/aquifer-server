using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Users.Create;

public class Request
{
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public UserRole Role { get; set; }
    public int? CompanyId { get; set; }
}