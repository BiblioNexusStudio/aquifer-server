namespace Aquifer.Common.Tiptap;

public class TiptapModel
{
    public TiptapRoot<TiptapRootContent> Tiptap { get; set; } = null!;
}

public class TiptapModel<TRootContent> where TRootContent : TiptapRootContent<TRootContent>
{
    public TiptapRoot<TRootContent> Tiptap { get; set; } = null!;
}

public class TiptapRoot<TRootContent>
{
    public string Type { get; set; } = null!;
    public List<TRootContent> Content { get; set; } = [];
}

public class TiptapRootContent<TRootContent>
{
    public virtual string Type { get; set; } = null!;
    public virtual TiptapAttributes? Attrs { get; set; }
    public virtual List<TiptapMark>? Marks { get; set; }
    public string Text { get; set; } = null!;
    public List<TRootContent>? Content { get; set; }
}

public class TiptapRootContent : TiptapRootContent<TiptapRootContent>
{
}

public class TiptapRootContentFiltered : TiptapRootContent<TiptapRootContentFiltered>
{
    private List<TiptapMark>? _marks;

    // Tiptap defaults to putting Type and Attrs ahead of Marks. But our serializer will put overrides at the top.
    // This is a stupid but easy way to keep Type and Attr up top so we can limit how much the JSON actually changes.
    // This is primarily out of an abundance of caution to prevent the auto-save kicking in if someone not
    // assigned to content loads it and then Tiptap reorganizes the JSON.
    public override string Type { get; set; } = null!;
    public override TiptapAttributes? Attrs { get; set; }

    public override List<TiptapMark>? Marks
    {
        get => _marks;
        set
        {
            var filteredMarks = value?.Where(x => x.Type != "comments").ToList();
            _marks = filteredMarks?.Count > 0 ? filteredMarks : null;
        }
    }
}

public class TiptapAttributes
{
    public string? Dir { get; set; }
    public int? Start { get; set; }
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