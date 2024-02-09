namespace Aquifer.API.Endpoints.Users.GetSelf;

public class Response
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required IEnumerable<string> Permissions { get; set; }

    public required CompanyResponse Company { get; set; }
}

public class CompanyResponse
{
    public int Id { get; set; }
}