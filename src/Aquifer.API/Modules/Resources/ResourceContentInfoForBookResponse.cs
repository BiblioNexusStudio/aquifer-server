using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources;

public class ResourceContentInfoForBookResponse
{
    public required IEnumerable<ResourceContentInfoForChapter> Chapters { get; set; }
}

public class ResourceContentInfoForChapter
{
    public int ChapterNumber { get; set; }
    public required IEnumerable<ResourceContentInfo> Contents { get; set; }
}

public class ResourceContentInfo
{
    public int ContentId { get; set; }
    public int ContentSize { get; set; }
    public ResourceContentMediaType MediaTypeName { get; set; }
    public string ParentResourceName { get; set; } = null!;
}