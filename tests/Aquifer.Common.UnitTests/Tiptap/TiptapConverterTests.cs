using Aquifer.Common.Tiptap;

namespace Aquifer.Common.UnitTests.Tiptap;

public sealed class TiptapConverterTests
{
    public static TheoryData<string, string[]> GetConversionData => new()
    {
        // test single step (test data taken from ResourceContentId 1432 in dev)
        {
            // lang=json
            """[{"tiptap":{"type":"doc","content":[{"type":"heading","attrs":{"level":1},"content":[{"type":"text","text":"AARON’S ROD*"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"Staff belonging to Moses’ brother, Aaron, symbolizing the two brothers’ authority in Israel. When the Israelites were wandering in the wilderness, a threat against Moses and Aaron’s leadership was led by Korah, Dathan, and Abiram ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1004016001,"endVerse":1004016040}]}}],"text":"Nm 16:1-40"},{"type":"text","text":"). In spite of the Lord’s destruction of those rebels and their followers, the rest of the people of Israel turned against Moses and Aaron, saying that they had killed the people of the Lord ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1004016041,"endVerse":1004016041}]}}],"text":"16:41"},{"type":"text","text":"). In order to restore respect for the divinely appointed leadership, the Lord told Moses to collect a rod from each tribe and have the leader of the tribe write his name on it. Aaron was told to write his name on the rod of Levi. The rods were placed in the inner room of the tabernacle, in front of the ark (of the covenant). In the morning, Aaron’s rod had sprouted blossoms and had produced ripe almonds. The rod was then kept there as a continual sign to Israel that the Lord had established the authority of Moses and Aaron ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1004017001,"endVerse":1004017011}]}}],"text":"Nm 17:1-11"},{"type":"text","text":"; cf. "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1059009004,"endVerse":1059009004}]}}],"text":"Heb 9:4"},{"type":"text","text":")."}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"Following that incident the people of Israel entered the wilderness of Zin, but there was no water for them and their flocks. Again the people argued with Moses and Aaron. The Lord instructed Moses to get the rod and, in the presence of Aaron and the rest of the people, command a particular rock to bring forth water. Taking the rod, Moses asked dramatically, “Must we bring you water from this rock?” ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1004020010,"endVerse":1004020010}]}}],"text":"Nm 20:10"},{"type":"text","text":", nlt) and struck the rock twice. Water gushed out and the people drank. Yet Moses and Aaron were forbidden to enter the Promised Land because they did not sanctify the Lord in the people’s eyes ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1004020012,"endVerse":1004020013}]}}],"text":"Nm 20:12-13"},{"type":"text","text":"). An earlier event had provided evidence that the Lord was able to provide needed water in that manner ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1002017001,"endVerse":1002017007}]}}],"text":"Ex 17:1-7"},{"type":"text","text":")."}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"مرحبا بالعالم"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","marks":[{"type":"italic"}],"text":"See also"},{"type":"text","text":" "},{"type":"text","marks":[{"type":"resourceReference","attrs":{"resourceId":"1242","resourceType":"TyndaleBibleDictionary"}}],"text":"Aaron"},{"type":"text","text":"."}]}]}}]""",
            [
                // lang=html
                """<h1>AARON’S ROD*</h1><p>Staff belonging to Moses’ brother, Aaron, symbolizing the two brothers’ authority in Israel. When the Israelites were wandering in the wilderness, a threat against Moses and Aaron’s leadership was led by Korah, Dathan, and Abiram (<span data-bnType="bibleReference" data-verses="[[1004016001,1004016040]]">Nm 16:1-40</span>). In spite of the Lord’s destruction of those rebels and their followers, the rest of the people of Israel turned against Moses and Aaron, saying that they had killed the people of the Lord (<span data-bnType="bibleReference" data-verses="[[1004016041,1004016041]]">16:41</span>). In order to restore respect for the divinely appointed leadership, the Lord told Moses to collect a rod from each tribe and have the leader of the tribe write his name on it. Aaron was told to write his name on the rod of Levi. The rods were placed in the inner room of the tabernacle, in front of the ark (of the covenant). In the morning, Aaron’s rod had sprouted blossoms and had produced ripe almonds. The rod was then kept there as a continual sign to Israel that the Lord had established the authority of Moses and Aaron (<span data-bnType="bibleReference" data-verses="[[1004017001,1004017011]]">Nm 17:1-11</span>; cf. <span data-bnType="bibleReference" data-verses="[[1059009004,1059009004]]">Heb 9:4</span>).</p><p>Following that incident the people of Israel entered the wilderness of Zin, but there was no water for them and their flocks. Again the people argued with Moses and Aaron. The Lord instructed Moses to get the rod and, in the presence of Aaron and the rest of the people, command a particular rock to bring forth water. Taking the rod, Moses asked dramatically, “Must we bring you water from this rock?” (<span data-bnType="bibleReference" data-verses="[[1004020010,1004020010]]">Nm 20:10</span>, nlt) and struck the rock twice. Water gushed out and the people drank. Yet Moses and Aaron were forbidden to enter the Promised Land because they did not sanctify the Lord in the people’s eyes (<span data-bnType="bibleReference" data-verses="[[1004020012,1004020013]]">Nm 20:12-13</span>). An earlier event had provided evidence that the Lord was able to provide needed water in that manner (<span data-bnType="bibleReference" data-verses="[[1002017001,1002017007]]">Ex 17:1-7</span>).</p><p>مرحبا بالعالم</p><p><em>See also</em> <span data-bnType="resourceReference" data-resourceId="1242" data-resourceType="TyndaleBibleDictionary">Aaron</span>.</p>"""
            ]
        },
        // test multiple steps (test data steps 1 and 2 taken from ResourceContentId 7441 in dev)
        {
            // lang=json
            """[{"stepNumber":1,"tiptap":{"type":"doc","content":[{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"请听《马可福音》7:9-13，并將這段經文放在心中。听三遍经文（如果可以，听三种不同的译文）。然后在小组中讨论下面的问题："}]},{"type":"orderedList","attrs":{"start":1},"content":[{"type":"listItem","content":[{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"你喜欢这个故事中的哪些部分？"}]}]},{"type":"listItem","content":[{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"有哪些是你不喜欢或者不理解的？"}]}]},{"type":"listItem","content":[{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"这个故事告诉我们关于耶稣的什么事？"}]}]},{"type":"listItem","content":[{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"这个故事告诉我们关于人们的什么事？"}]}]},{"type":"listItem","content":[{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"这个故事如何影响我们的日常生活？"}]}]},{"type":"listItem","content":[{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"你知道有谁应该听一听这个故事吗？"}]}]}]}]}},{"stepNumber":2,"tiptap":{"type":"doc","content":[{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"设定场景"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"《马可福音》7:9-13"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"听一遍最简单易懂版本的经文。"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"在我们上次的讨论，耶稣告诉宗教领袖，比起他们添加并要人们遵守的口传律法，神的诫命更为重要。在这里，他举了一个人创法则违反神的道的具体例子。"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"耶稣阐述宗教权威的口传律法允许百姓逃避十诫中孝敬父母的戒律。摩西在旧约中两次提到孝敬父母："}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"1.第一次是在十诫第五条，他指出当孝敬父母使得人们在世上长寿。"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"（2） 第二次，摩西说对父母不敬的人应被处死。"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"但宗教领袖们却说，如果百姓已经起誓向神奉献一笔财物，就不必奉养父母。如果有人立这种特殊的誓言（称作各耳板）把金钱归神，即使父母需要，"},{"type":"text","marks":[{"type":"italic"}],"text":"也不该"},{"type":"text","text":"供给他们，因为百姓不得背弃向神许的誓词。在起各耳板的誓後的確不得食言。而且神比包括父母在内的任何人更重要。但是这里耶稣的意思是说，不应该用神的律法来违反神的律法。神的戒律不是这么用的。耶稣提到宗教权威常做这种事。"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"请暂停一下分组讨论：在你的文化里，有没有宗教领袖为了自利而曲解教条的例子？"}]}]}}]""",
            [
                // Step 1
                // lang=html
                "<p>请听《马可福音》7:9-13，并將這段經文放在心中。听三遍经文（如果可以，听三种不同的译文）。然后在小组中讨论下面的问题：</p><ol><li><p>你喜欢这个故事中的哪些部分？</p></li><li><p>有哪些是你不喜欢或者不理解的？</p></li><li><p>这个故事告诉我们关于耶稣的什么事？</p></li><li><p>这个故事告诉我们关于人们的什么事？</p></li><li><p>这个故事如何影响我们的日常生活？</p></li><li><p>你知道有谁应该听一听这个故事吗？</p></li></ol>",
                // Step 2
                // lang=html
                "<p>设定场景</p><p>《马可福音》7:9-13</p><p>听一遍最简单易懂版本的经文。</p><p>在我们上次的讨论，耶稣告诉宗教领袖，比起他们添加并要人们遵守的口传律法，神的诫命更为重要。在这里，他举了一个人创法则违反神的道的具体例子。</p><p>耶稣阐述宗教权威的口传律法允许百姓逃避十诫中孝敬父母的戒律。摩西在旧约中两次提到孝敬父母：</p><p>1.第一次是在十诫第五条，他指出当孝敬父母使得人们在世上长寿。</p><p>（2） 第二次，摩西说对父母不敬的人应被处死。</p><p>但宗教领袖们却说，如果百姓已经起誓向神奉献一笔财物，就不必奉养父母。如果有人立这种特殊的誓言（称作各耳板）把金钱归神，即使父母需要，<em>也不该</em>供给他们，因为百姓不得背弃向神许的誓词。在起各耳板的誓後的確不得食言。而且神比包括父母在内的任何人更重要。但是这里耶稣的意思是说，不应该用神的律法来违反神的律法。神的戒律不是这么用的。耶稣提到宗教权威常做这种事。</p><p>请暂停一下分组讨论：在你的文化里，有没有宗教领袖为了自利而曲解教条的例子？</p>"
            ]
        },
    };

    [Theory]
    [MemberData(nameof(GetConversionData))]
    public void TiptapConverter_ConvertJsonToHtmlItems_Success(string json, IReadOnlyList<string> htmlItems)
    {
        var convertedHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(json);
        Assert.Equal(htmlItems, convertedHtmlItems);
    }

    [Theory]
    [MemberData(nameof(GetConversionData))]
    public void TiptapConverter_ConvertHtmlItemsToJson_Success(string json, IReadOnlyList<string> htmlItems)
    {
        var convertedJson = TiptapConverter.ConvertHtmlItemsToJson(htmlItems);
        Assert.Equal(json, convertedJson);
    }

    [Theory]
    [MemberData(nameof(GetConversionData))]
    public void TiptapConverter_JsonRoundtrip_Success(string json, IReadOnlyList<string> _)
    {
        var convertedHtmlItems = TiptapConverter.ConvertJsonToHtmlItems(json);
        var convertedJson = TiptapConverter.ConvertHtmlItemsToJson(convertedHtmlItems);
        Assert.Equal(json, convertedJson);
    }

    [Theory]
    [MemberData(nameof(GetConversionData))]
    public void TiptapConverter_HtmlRoundtrip_Success(string _, IReadOnlyList<string> htmlItems)
    {
        var convertedJson = TiptapConverter.ConvertHtmlItemsToJson(htmlItems);
        var convertedHtml = TiptapConverter.ConvertJsonToHtmlItems(convertedJson);
        Assert.Equal(htmlItems, convertedHtml);
    }
}