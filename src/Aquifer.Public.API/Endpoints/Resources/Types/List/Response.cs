namespace Aquifer.Public.API.Endpoints.Resources.Types.List;

public class Response
{
    public required string Type { get; set; }
    public List<AvailableResourceTitles> Titles { get; set; } = [];
}

public class AvailableResourceTitles
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public object? LicenseInformation { get; set; }
}