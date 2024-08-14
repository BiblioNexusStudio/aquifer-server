using System.ComponentModel;
using Aquifer.Common.Tiptap;

namespace Aquifer.Public.API.Endpoints.Resources.Get;

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
    ///     version with mention of Tiptap removed. `Markdown` and 'Html' can also be requested.
    ///     <br />
    ///     Note that this will be ignored for non-text resources. Content such as images will always return as JSON.
    /// </summary>
    [DefaultValue(TiptapContentType.None)]
    public TiptapContentType ContentTextType { get; init; }

    /// <summary>
    ///     Optional parameter to supply an alternate language code (e.g. eng) that the given content id should be sent back in. For example,
    ///     if you have the English content id for a specific item, and you want to get the French version, pass "fra" as the value. To ensure
    ///     the fastest response time, it's recommended to use the content id of the language you want if you have it.
    ///     If no version exists in the requested language, a 404 will be returned.
    /// </summary>
    [DefaultValue(null)]
    public string? AsLanguage { get; init; }
}