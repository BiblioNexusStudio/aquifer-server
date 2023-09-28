namespace Aquifer.API.Modules.Bibles;

public class BibleResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;

    public IEnumerable<BibleResponseBook> Books { get; set; } =
        new List<BibleResponseBook>();
}

public class BibleResponseBook
{
    public string BookCode { get; set; }
    public string DisplayName { get; set; } = null!;
    public int TextSize { get; set; }
    public int AudioSize { get; set; }
    public int ChapterCount { get; set; }
}

public class BibleBookDetailsResponse : BibleResponseBook
{
    public string TextUrl { get; set; } = null!;
    public object? AudioUrls { get; set; }
}