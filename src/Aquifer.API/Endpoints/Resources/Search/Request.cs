using System.ComponentModel;
using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Resources.Search;

public record Request
{
    public int LanguageId { get; set; }
    public string Query { get; set; } = null!;
    public List<ResourceType> ResourceTypes { get; set; } = [];
    public string? BookCode { get; init; }

    [DefaultValue(0)]
    public int StartChapter { get; init; }

    [DefaultValue(0)]
    public int EndChapter { get; init; }

    [DefaultValue(0)]
    public int StartVerse { get; init; }

    [DefaultValue(0)]
    public int EndVerse { get; init; }
}