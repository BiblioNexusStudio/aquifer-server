using Aquifer.API.Utilities;
using System.Text.Json.Serialization;

namespace Aquifer.API.Modules.Bibles;

public class BasicBibleResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }

    public object? LicenseInfo =>
        SerializedLicenseInfo == null ? null : JsonUtilities.DefaultDeserialize(SerializedLicenseInfo);

    [JsonIgnore]
    public string? SerializedLicenseInfo { get; init; }
}

public class BibleResponse : BasicBibleResponse
{
    public required IEnumerable<BibleResponseBook> Books { get; set; }
}

public class BibleResponseBook
{
    public required string BookCode { get; set; }
    public required string DisplayName { get; set; }
    public int TextSize { get; set; }
    public int AudioSize { get; set; }
    public int ChapterCount { get; set; }
}

public class BibleBookDetailsResponse : BibleResponseBook
{
    public string TextUrl { get; set; } = null!;
    public object? AudioUrls { get; set; }
}