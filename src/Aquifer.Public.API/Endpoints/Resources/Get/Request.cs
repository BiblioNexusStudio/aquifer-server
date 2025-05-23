﻿using System.ComponentModel;
using Aquifer.Common.Tiptap;

namespace Aquifer.Public.API.Endpoints.Resources.Get;

public record Request
{
    /// <summary>
    /// The resource id to return content for. This is included at the end of the path.
    /// </summary>
    public int ContentId { get; init; }

    /// <summary>
    /// The type of text content to return in the `content` property of the response (JSON is always sent back for the response as a
    /// whole by default). This parameter is optional and defaults to `None`.
    /// <br />
    /// If `None` is passed then `Json` will be returned.  `Markdown` and 'Html' can also be requested.
    /// <br />
    /// Note that this will be ignored for non-text resources. Content such as images will always return as JSON.
    /// </summary>
    [DefaultValue(TiptapContentType.None)]
    public TiptapContentType ContentTextType { get; init; }
}