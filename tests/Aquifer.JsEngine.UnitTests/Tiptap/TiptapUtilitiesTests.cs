using Aquifer.JsEngine.Tiptap;

namespace Aquifer.JsEngine.UnitTests.Tiptap;

public sealed class TiptapUtilitiesTests
{
    [Fact]
    public void ParseJsonAsHtml_JavaScriptMethodExists()
    {
        // lang=json
        const string noContentTiptapModelInArray = """[{"tiptap":{"type":"doc","content":[]}}]""";

        Assert.Null(
            Record.Exception(() =>
            {
                using TiptapUtilities tiptapConverter = new();
                return tiptapConverter.ParseJsonAsHtml(noContentTiptapModelInArray);
            }));
    }

    [Fact]
    public void ParseHtmlAsJson_JavaScriptMethodExists()
    {
        Assert.Null(
            Record.Exception(() =>
            {
                using TiptapUtilities tiptapConverter = new();
                return tiptapConverter.ParseHtmlAsJson("");
            }));
    }
}