using Aquifer.API.Utilities;
using Aquifer.Data.Enums;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Bibles;

public class BibleResponse
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

public class BibleWithBooksMetadataResponse : BibleResponse
{
    public required IEnumerable<BibleBookMetadataResponse> Books { get; set; }
}

public class BibleBookMetadataResponse
{
    public required string BookCode { get; set; }
    public required string DisplayName { get; set; }
    public int TextSize { get; set; }
    public int AudioSize { get; set; }
    public int ChapterCount { get; set; }
}

public class BibleBookResponse : BibleBookMetadataResponse
{
    public string TextUrl { get; set; } = null!;
    public object? AudioUrls { get; set; }
}
