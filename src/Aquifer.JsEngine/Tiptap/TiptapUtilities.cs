using JavaScriptEngineSwitcher.V8;

namespace Aquifer.JsEngine.Tiptap;

/// <summary>
/// This class is purely a C# wrapper around TiptapUtilities.js.
/// See also Aquifer.Tiptap where that file is generated.
/// </summary>
public sealed class TiptapUtilities : IDisposable
{
    private readonly V8JsEngine _jsEngine;

    public TiptapUtilities()
    {
        _jsEngine = new V8JsEngine();
        _jsEngine.ExecuteResource($"{GetType().Namespace}.TiptapUtilities.js", GetType().Assembly);
    }

    public void Dispose()
    {
        _jsEngine.Dispose();
    }

    /// <summary>
    /// Note: Resulting JSON will not be inside a Tiptap model array.
    /// This means that round-tripping JSON -> HTML -> JSON will not match the original JSON.
    /// </summary>
    public string ParseHtmlAsJson(string sourceHtml)
    {
        _jsEngine.SetVariableValue("html", sourceHtml);
        _jsEngine.Execute("json = parseHtmlAsJson(html)");
        return _jsEngine.Evaluate<string>("json");
    }

    public string ParseJsonAsHtml(string sourceJson)
    {
        _jsEngine.SetVariableValue("json", sourceJson);
        _jsEngine.Execute("html = parseJsonAsHtml(json)");
        return _jsEngine.Evaluate<string>("html");
    }
}