using Aquifer.Common.Utilities;

namespace Aquifer.Common.UnitTests;

public sealed class HtmlUtilitiesTests
{
    public static IEnumerable<object[]> GetConversionData =>
    [
        [
            (
                Html: /* lang=html */ """
                    <body>
                        <div>
                            <p>
                            <!-- This is a comment.-->
                                Test  &nbsp;   *HTML.
                            </p>
                        </div>
                    </body>
                    """,
                PlainText: "Test  \u00a0   *HTML.",
                WordCount: 2
            ),
        ],
        [
            (
                Html: /* lang=html */ """<div><span>This is <span>important</span> text.</span></div>""",
                PlainText: "This is important text.",
                WordCount: 4
            ),
        ],
        [
            (Html: /* lang=html */ """<h1>AARON’S <b>ROD</b></h1><p>Staff <br /> ... Abiram (<span data-bnType="bibleReference" data-verses="[[1004016001,1004016040]]">Nm 16:1-40</span>).</p><p>مرحبا بالعالم</p><p><em>See also</em> <span data-bnType="resourceReference" data-resourceId="1242" data-resourceType="TyndaleBibleDictionary">Aaron</span>.</p>""",
                PlainText: "AARON’S ROD Staff ... Abiram ( Nm 16:1-40 ). مرحبا بالعالم See also Aaron .",
                WordCount: 11),
        ],
    ];

    [Theory]
    [MemberData(nameof(GetConversionData))]
    public void GetPlainText_Success((string Html, string PlainText, int _) data)
    {
        var convertedPlainText = HtmlUtilities.GetPlainText(data.Html);
        Assert.Equal(data.PlainText, convertedPlainText);
    }

    [Theory]
    [MemberData(nameof(GetConversionData))]
    public void GetWordCount_Success((string Html, string _, int WordCount) data)
    {
        var wordCount = HtmlUtilities.GetWordCount(data.Html);
        Assert.Equal(data.WordCount, wordCount);
    }
}