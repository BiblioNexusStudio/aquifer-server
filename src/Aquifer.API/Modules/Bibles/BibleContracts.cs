using Aquifer.API.Utilities;
using Aquifer.Data.Enums;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Bibles;

public class BibleDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public int LanguageId { get; set; }

    public object? LicenseInfo =>
        SerializedLicenseInfo == null ? null : JsonUtilities.DefaultDeserialize(SerializedLicenseInfo);

    [JsonIgnore]
    public string? SerializedLicenseInfo { get; init; }
}

public class BibleWithBooksMetadataDto : BibleDto
{
    public required IEnumerable<BibleBookMetadataDto> Books { get; set; }
}

public class BibleBookMetadataDto
{
    public BookCode BookCode { get; set; }
    public required string DisplayName { get; set; }
    public int TextSize { get; set; }
    public int AudioSize { get; set; }
    public int ChapterCount { get; set; }
}

public class BibleBookDto : BibleBookMetadataDto
{
    public string TextUrl { get; set; } = null!;
    public object? AudioUrls { get; set; }
}