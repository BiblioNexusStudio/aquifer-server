using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources;

public class ResourceItemsByChapterResponse
{
    public required IEnumerable<ResourceItemsWithChapterNumberResponse> Chapters { get; set; }
}

public class ResourceItemsWithChapterNumberResponse
{
    public int ChapterNumber { get; set; }
    public required IEnumerable<ResourceItemResponse> Contents { get; set; }
}

public class ResourceItemResponse
{
    public int ContentId { get; set; }
    public string ParentResourceName { get; set; } = null!;
    public ResourceContentMediaType MediaTypeName { get; set; }
    public int ContentSize { get; set; }
}

public class ResourceItemTextContentResponse
{
    public int Id { get; set; }
    public object? Content { get; set; }
}

public class ResourceItemMetadataResponse
{
    public string? DisplayName { get; set; }
    public object? Metadata { get; set; }
}

public class ResourceItemMetadataWithIdResponse : ResourceItemMetadataResponse
{
    public int Id { get; set; }
}