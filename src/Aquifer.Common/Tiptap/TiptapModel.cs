namespace Aquifer.Common.Tiptap;

public class TiptapModel
{
    public TiptapRoot Tiptap { get; set; } = null!;
}

public class TiptapRoot
{
    public string Type { get; set; } = null!;
    public List<TiptapRootContent> Content { get; set; } = [];
}

public class TiptapRootContent
{
    public string Type { get; set; } = null!;
    public string Text { get; set; } = null!;
    public TiptapAttributes? Attrs { get; set; }
    public List<TiptapMark>? Marks { get; set; }
    public List<TiptapRootContent>? Content { get; set; }
}

public class TiptapAttributes
{
    public int? Level { get; set; }

    public List<TiptapVersesAttribute>? Verses { get; set; }

    // Some ResourceIds weren't properly set to an id in our db, and are still strings. This makes both string and int work.
    // This isn't a good long term fix, the data needs to be corrected.
    public object? ResourceId { get; set; }
    public string? ResourceType { get; set; }
}

public class TiptapMark
{
    public string Type { get; set; } = null!;
    public string Text { get; set; } = null!;
    public TiptapAttributes? Attrs { get; set; }
}

public class TiptapVersesAttribute
{
    public object? StartVerse { get; set; }
    public object? EndVerse { get; set; }
}