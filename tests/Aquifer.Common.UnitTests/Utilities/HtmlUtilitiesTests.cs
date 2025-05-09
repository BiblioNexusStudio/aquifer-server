using Aquifer.Common.Utilities;

namespace Aquifer.Common.UnitTests.Utilities;

public sealed class HtmlUtilitiesTests
{
    public static TheoryData<string, string, int> GetHtmlToPlainTextConversionTestData => new()
    {
        {
            /* lang=html */ """
            <body>
                <div>
                    <p>
                    <!-- This is a comment.-->
                        Test  &nbsp;   *HTML.
                    </p>
                </div>
            </body>
            """,
            "Test  \u00a0   *HTML.", 2
        },
        {
            /* lang=html */ """<div><span>This is <span>important</span> text.</span></div>""", "This is important text.", 4
        },
        {
            /* lang=html */
            """<h1>AARON’S <b>ROD</b></h1><p>Staff <br /> ... Abiram (<span data-bnType="bibleReference" data-verses="[[1004016001,1004016040]]">Nm 16:1-40</span>).</p><p>مرحبا بالعالم</p><p><em>See also</em> <span data-bnType="resourceReference" data-resourceId="1242" data-resourceType="TyndaleBibleDictionary">Aaron</span>.</p>""",
            "AARON’S ROD Staff ... Abiram ( Nm 16:1-40 ). مرحبا بالعالم See also Aaron .", 11
        },
    };

    [Theory]
    [MemberData(nameof(GetHtmlToPlainTextConversionTestData))]
    public void GetPlainText_Success(string html, string plainText, int _)
    {
        var convertedPlainText = HtmlUtilities.GetPlainText(html);
        Assert.Equal(plainText, convertedPlainText);
    }

    [Theory]
    [MemberData(nameof(GetHtmlToPlainTextConversionTestData))]
    public void GetWordCount_Success(string html, string _, int wordCount)
    {
        var actualWordCount = HtmlUtilities.GetWordCount(html);
        Assert.Equal(wordCount, actualWordCount);
    }

    [Theory]
    [InlineData(
        /* lang=html */
        """<h1>AARON’S <b>ROD</b></h1><p>Staff <br /> ... Abiram (<span data-bnType="bibleReference" data-verses="[[1004016001,1004016040]]">Nm 16:1-40</span>).</p><p>مرحبا بالعالم</p><p><em>See also</em> <span data-bnType="resourceReference" data-resourceId="1242" data-resourceType="TyndaleBibleDictionary">Aaron</span>.</p>""",
        /* lang=html */
        """<h1>AARON’S <b>ROD</b></h1><p>Staff <br /> ... Abiram (<span a="0">Nm 16:1-40</span>).</p><p>مرحبا بالعالم</p><p><em>See also</em> <span a="1">Aaron</span>.</p>""")]
    public async Task ProcessHtmlContentAsync_Success(string html, string expectedMinifiedHtml)
    {
        var roundTrippedHtml = await HtmlUtilities.ProcessHtmlContentAsync(
            html,
            minifiedHtml =>
            {
                Assert.Equal(expectedMinifiedHtml, minifiedHtml);
                return Task.FromResult(minifiedHtml);
            });

        Assert.Equal(html, roundTrippedHtml);
    }

    [Theory]
    [InlineData( /* lang=html */
        """<p><strong>«&nbsp;la maison d'Israël »</strong></p><p>Ici, <strong>maison</strong> représente un groupe de personnes, les Israélites, qui descendaient de Jacob, qui était aussi nommé Israël. Si cela est utile dans votre langue, vous pourriez utiliser une métaphore de votre langue ou traduire le sens. C'est une métaphore biblique courante, donc vous pourriez vouloir vérifier d'autres endroits où cela se produit. La <strong>maison d'Israël</strong> est équivalente à « fils d'Israël » ou « Israélites ».</p><p>Voir&nbsp;: <span data-bnType="resourceReference" data-resourceId="26706" data-resourceType="UWTranslationManual">Métaphore</span></p>""",
        /* lang=html */
        """<p><strong>«&nbsp;la maison d'Israël »</strong></p><p>Ici, <strong>maison</strong> représente un groupe de personnes, les Israélites, qui descendaient de Jacob, qui était aussi nommé Israël. Si cela est utile dans votre langue, vous pourriez utiliser une métaphore de votre langue ou traduire le sens. C'est une métaphore biblique courante, donc vous pourriez vouloir vérifier d'autres endroits où cela se produit. La <strong>maison d'Israël</strong> est équivalente à « fils d'Israël » ou « Israélites ».</p><p>Voir&nbsp;: <span data-bntype="resourceReference" data-resourceid="26706" data-resourcetype="UWTranslationManual">Métaphore</span></p>""")]
    public async Task ProcessHtmlTextContentAsync_Success(string html, string expectedHtml)
    {
        var roundTrippedHtml = await HtmlUtilities.ProcessHtmlTextContentAsync(
            html,
            text =>
            {
                text = text.Replace("\u202f", "\u00a0").Replace("« ", "«\u00a0").Replace(" »", "\u00a0»");
                return Task.FromResult(text);
            });

        Assert.Equal(expectedHtml, roundTrippedHtml);
    }
}