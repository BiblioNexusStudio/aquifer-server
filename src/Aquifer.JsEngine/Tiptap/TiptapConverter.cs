using JavaScriptEngineSwitcher.V8;

namespace Aquifer.JsEngine.Tiptap;

public interface ITiptapConverter
{
    string FormatHtmlAsJson(string sourceHtml);
    string FormatJsonAsHtml(string sourceJson);
}

public sealed class TiptapConverter : ITiptapConverter, IDisposable
{
    private readonly V8JsEngine _jsEngine;

    public TiptapConverter()
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
    /// Use <see cref="WrapJsonWithTiptapModelArray"/> to wrap the return value of this method if a Tiptap model array is needed.
    /// </summary>
    public string FormatHtmlAsJson(string sourceHtml)
    {
        _jsEngine.SetVariableValue("html", sourceHtml);
        _jsEngine.Execute("json = parseHtmlAsJson(html)");
        return _jsEngine.Evaluate<string>("json");
    }

    public string FormatJsonAsHtml(string sourceJson)
    {
        _jsEngine.SetVariableValue("json", sourceJson);
        _jsEngine.Execute("html = parseJsonAsHtml(json)");
        return _jsEngine.Evaluate<string>("html");
    }

    public static string WrapJsonWithTiptapModelArray(string json)
    {
        return $"[{{\"tiptap\":{json}}}]";
    }
}