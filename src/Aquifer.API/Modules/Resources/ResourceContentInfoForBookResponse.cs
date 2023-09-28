using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources;

public class ResourceContentInfoForBookResponse
{
    public IEnumerable<ResourceContentInfoForChapter> Chapters { get; set; } = null!;
}

public class ResourceContentInfoForChapter
{
    public int ChapterNumber { get; set; }
    public IEnumerable<ResourceContentInfo> Contents { get; set; } = null!;
}

public class ResourceContentInfo
{
    public int ContentId { get; set; }
    public int ContentSize { get; set; }
    public ResourceContentMediaType MediaType { get; set; }
    public ResourceEntityType Type { get; set; }
}