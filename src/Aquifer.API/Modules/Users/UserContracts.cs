namespace Aquifer.API.Modules.Users;

public class UserResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class CurrentUserResponse : UserResponse
{
    public required IEnumerable<string> Permissions { get; set; }
}

public class CreateUserRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Role { get; set; } = null!;
}