using Aquifer.JsEngine.Tiptap;

namespace Aquifer.JsEngine.UnitTests;

public sealed class TiptapConverterTests
{
    public static IEnumerable<object[]> GetConversionData =>
    [
        [
            // test data taken from ResourceContentId 1432 in dev
            (Json: /* lang=json */ """[{"tiptap":{"type":"doc","content":[{"type":"heading","attrs":{"level":1},"content":[{"type":"text","text":"AARON’S ROD*"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"Staff belonging to Moses’ brother, Aaron, symbolizing the two brothers’ authority in Israel. When the Israelites were wandering in the wilderness, a threat against Moses and Aaron’s leadership was led by Korah, Dathan, and Abiram ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1004016001,"endVerse":1004016040}]}}],"text":"Nm 16:1-40"},{"type":"text","text":"). In spite of the Lord’s destruction of those rebels and their followers, the rest of the people of Israel turned against Moses and Aaron, saying that they had killed the people of the Lord ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1004016041,"endVerse":1004016041}]}}],"text":"16:41"},{"type":"text","text":"). In order to restore respect for the divinely appointed leadership, the Lord told Moses to collect a rod from each tribe and have the leader of the tribe write his name on it. Aaron was told to write his name on the rod of Levi. The rods were placed in the inner room of the tabernacle, in front of the ark (of the covenant). In the morning, Aaron’s rod had sprouted blossoms and had produced ripe almonds. The rod was then kept there as a continual sign to Israel that the Lord had established the authority of Moses and Aaron ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1004017001,"endVerse":1004017011}]}}],"text":"Nm 17:1-11"},{"type":"text","text":"; cf. "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1059009004,"endVerse":1059009004}]}}],"text":"Heb 9:4"},{"type":"text","text":")."}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"Following that incident the people of Israel entered the wilderness of Zin, but there was no water for them and their flocks. Again the people argued with Moses and Aaron. The Lord instructed Moses to get the rod and, in the presence of Aaron and the rest of the people, command a particular rock to bring forth water. Taking the rod, Moses asked dramatically, “Must we bring you water from this rock?” ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1004020010,"endVerse":1004020010}]}}],"text":"Nm 20:10"},{"type":"text","text":", nlt) and struck the rock twice. Water gushed out and the people drank. Yet Moses and Aaron were forbidden to enter the Promised Land because they did not sanctify the Lord in the people’s eyes ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1004020012,"endVerse":1004020013}]}}],"text":"Nm 20:12-13"},{"type":"text","text":"). An earlier event had provided evidence that the Lord was able to provide needed water in that manner ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1002017001,"endVerse":1002017007}]}}],"text":"Ex 17:1-7"},{"type":"text","text":")."}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"مرحبا بالعالم"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","marks":[{"type":"italic"}],"text":"See also"},{"type":"text","text":" "},{"type":"text","marks":[{"type":"resourceReference","attrs":{"resourceId":"1242","resourceType":"TyndaleBibleDictionary"}}],"text":"Aaron"},{"type":"text","text":"."}]}]}}]""",
                Html: /* lang=html */ """<h1>AARON’S ROD*</h1><p>Staff belonging to Moses’ brother, Aaron, symbolizing the two brothers’ authority in Israel. When the Israelites were wandering in the wilderness, a threat against Moses and Aaron’s leadership was led by Korah, Dathan, and Abiram (<span data-bnType="bibleReference" data-verses="[[1004016001,1004016040]]">Nm 16:1-40</span>). In spite of the Lord’s destruction of those rebels and their followers, the rest of the people of Israel turned against Moses and Aaron, saying that they had killed the people of the Lord (<span data-bnType="bibleReference" data-verses="[[1004016041,1004016041]]">16:41</span>). In order to restore respect for the divinely appointed leadership, the Lord told Moses to collect a rod from each tribe and have the leader of the tribe write his name on it. Aaron was told to write his name on the rod of Levi. The rods were placed in the inner room of the tabernacle, in front of the ark (of the covenant). In the morning, Aaron’s rod had sprouted blossoms and had produced ripe almonds. The rod was then kept there as a continual sign to Israel that the Lord had established the authority of Moses and Aaron (<span data-bnType="bibleReference" data-verses="[[1004017001,1004017011]]">Nm 17:1-11</span>; cf. <span data-bnType="bibleReference" data-verses="[[1059009004,1059009004]]">Heb 9:4</span>).</p><p>Following that incident the people of Israel entered the wilderness of Zin, but there was no water for them and their flocks. Again the people argued with Moses and Aaron. The Lord instructed Moses to get the rod and, in the presence of Aaron and the rest of the people, command a particular rock to bring forth water. Taking the rod, Moses asked dramatically, “Must we bring you water from this rock?” (<span data-bnType="bibleReference" data-verses="[[1004020010,1004020010]]">Nm 20:10</span>, nlt) and struck the rock twice. Water gushed out and the people drank. Yet Moses and Aaron were forbidden to enter the Promised Land because they did not sanctify the Lord in the people’s eyes (<span data-bnType="bibleReference" data-verses="[[1004020012,1004020013]]">Nm 20:12-13</span>). An earlier event had provided evidence that the Lord was able to provide needed water in that manner (<span data-bnType="bibleReference" data-verses="[[1002017001,1002017007]]">Ex 17:1-7</span>).</p><p>مرحبا بالعالم</p><p><em>See also</em> <span data-bnType="resourceReference" data-resourceId="1242" data-resourceType="TyndaleBibleDictionary">Aaron</span>.</p>"""),
        ],
    ];

    [Fact]
    public void TipTapConverter_FormatJsonAsHtml_JavaScriptMethodExists()
    {
        Assert.Null(Record.Exception(() =>
        {
            using TiptapConverter tiptapConverter = new();
            return tiptapConverter.FormatJsonAsHtml(/* lang=json */ """[{"tiptap":{"type":"doc","content":[]}}]""");
        }));
    }

    [Fact]
    public void TipTapConverter_FormatHtmlAsJson_JavaScriptMethodExists()
    {
        Assert.Null(Record.Exception(() =>
        {
            using TiptapConverter tiptapConverter = new();
            return tiptapConverter.FormatHtmlAsJson("");
        }));
    }

    [Theory]
    [MemberData(nameof(GetConversionData))]
    public void TiptapConverter_FormatJsonAsHtml_Success((string Json, string Html) data)
    {
        using TiptapConverter tiptapConverter = new();
        var convertedHtml = tiptapConverter.FormatJsonAsHtml(data.Json);
        Assert.Equal(data.Html.Trim(), convertedHtml.Trim());
    }

    [Theory]
    [MemberData(nameof(GetConversionData))]
    public void TiptapConverter_FormatHtmlAsJson_Success((string Json, string Html) data)
    {
        using TiptapConverter tiptapConverter = new();
        var convertedJson = TiptapConverter.WrapJsonWithTiptapModelArray(tiptapConverter.FormatHtmlAsJson(data.Html));
        Assert.Equal(data.Json.Trim(), convertedJson.Trim());
    }

    [Theory]
    [MemberData(nameof(GetConversionData))]
    public void TiptapConverter_JsonRoundtrip_Success((string Json, string _) data)
    {
        using TiptapConverter tiptapConverter = new();
        var convertedHtml = tiptapConverter.FormatJsonAsHtml(data.Json);
        var convertedJson = TiptapConverter.WrapJsonWithTiptapModelArray(tiptapConverter.FormatHtmlAsJson(convertedHtml));
        Assert.Equal(data.Json.Trim(), convertedJson.Trim());
    }

    [Theory]
    [MemberData(nameof(GetConversionData))]
    public void TiptapConverter_HtmlRoundtrip_Success((string _, string Html) data)
    {
        using TiptapConverter tiptapConverter = new();
        var convertedJson = TiptapConverter.WrapJsonWithTiptapModelArray(tiptapConverter.FormatHtmlAsJson(data.Html));
        var convertedHtml = tiptapConverter.FormatJsonAsHtml(convertedJson);
        Assert.Equal(data.Html.Trim(), convertedHtml.Trim());
    }
}