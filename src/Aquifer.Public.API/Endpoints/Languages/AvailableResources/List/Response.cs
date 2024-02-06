namespace Aquifer.Public.API.Endpoints.Languages.AvailableResources.List;

public class Response
{
    public int LanguageId { get; set; }
    public required string LanguageCode { get; set; }
    public List<ResourceCountByType> ResourceCounts { get; set; } = [];
}

public class ResourceCountByType
{
    public required string Type { get; set; }
    public int Count { get; set; }
}