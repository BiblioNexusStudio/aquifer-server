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