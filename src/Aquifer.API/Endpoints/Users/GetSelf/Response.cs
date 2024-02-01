namespace Aquifer.API.Endpoints.Users.GetSelf;

public class Response
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required IEnumerable<string> Permissions { get; set; }
    public required IEnumerable<string> Roles { get; set; }
}