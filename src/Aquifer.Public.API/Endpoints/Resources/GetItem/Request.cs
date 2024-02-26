using System.ComponentModel;
using Aquifer.Common.Tiptap;

namespace Aquifer.Public.API.Endpoints.Resources.GetItem;

public record Request
{
    /// <summary>
    ///     The resource id to return content for. This is included at the end of the path.
    /// </summary>
    public int ContentId { get; init; }

    /// <summary>
    ///     The type of text content to return in the `content` property of the response (JSON is always sent back for the response as a
    ///     whole by default). This parameter is optional and defaults to `None`.
    ///     <br />
    ///     If `None` is passed, it wil return JSON specifically for the Tiptap Editor, if `Json` is specified, it will return a simplified
    ///     version with mention of Tiptap removed. `Markdown` can also be requested. HTML will be an option in the future.
    /// </summary>
    [DefaultValue(TiptapContentType.None)]
    public TiptapContentType ContentTextType { get; init; }
}