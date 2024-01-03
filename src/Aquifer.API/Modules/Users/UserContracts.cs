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

public class UserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class TokenResponse
{
    public string access_token { get; set; }
}

public class CreateUserResponse
{
    public string user_id { get; set; }
}