﻿using System.Text.Json.Serialization;

namespace Aquifer.Common.Tiptap;

public class TiptapModel<TRootContent> where TRootContent : TiptapNode<TRootContent>
{
    public TiptapRoot<TRootContent> Tiptap { get; set; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? StepNumber { get; set; }
}

public class TiptapRoot<TRootContent>
{
    public string Type { get; set; } = null!;
    public List<TRootContent> Content { get; set; } = [];
}

public class TiptapNode<TRootContent>
{
    public virtual string Type { get; set; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual object? Attrs { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual List<TiptapMark>? Marks { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TRootContent>? Content { get; set; }
}

public class TiptapNode : TiptapNode<TiptapNode>
{
}

public class TiptapNodeFiltered : TiptapNode<TiptapNodeFiltered>
{
    private List<TiptapMark>? _marks;

    // Tiptap defaults to putting Type and Attrs ahead of Marks. But our serializer will put overrides at the top.
    // This is a stupid but easy way to keep Type and Attr up top so we can limit how much the JSON actually changes.
    // This is primarily out of an abundance of caution to prevent the auto-save kicking in if someone not
    // assigned to content loads it and then Tiptap reorganizes the JSON.
    public override string Type { get; set; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override object? Attrs { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
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

public class TiptapMark
{
    public string Type { get; set; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Attrs { get; set; }
}