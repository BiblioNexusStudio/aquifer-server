namespace Aquifer.API.Modules.Bibles;

public class BibleResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }

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