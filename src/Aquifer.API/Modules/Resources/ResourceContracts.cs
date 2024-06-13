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
    public int ParentResourceId { get; set; }
    public int Version { get; set; }
    public ResourceContentMediaType MediaTypeName { get; set; }
    public int ContentSize { get; set; }
}

public class ResourceItemTextContentResponse
{
    public int Id { get; set; }
    public object? Content { get; set; }
}