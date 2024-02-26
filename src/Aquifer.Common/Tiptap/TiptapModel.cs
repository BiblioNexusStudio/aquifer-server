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
    public int? ResourceId { get; set; }
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
    public int StartVerse { get; set; }
    public int EndVerse { get; set; }
}