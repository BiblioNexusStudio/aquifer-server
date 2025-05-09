using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;

namespace Aquifer.Common.IntegrationTests.Utilities;

public sealed class VersificationUtilitiesTests(App _app) : TestBase<App>
{
    private const int EngVersificationSchemeBibleId = 0;
    private const int BsbBibleId = 1;
    private const int Ls1910BibleId = 6;
    private const int RsbBibleId = 9;
    private const int IrvBibleId = 2;
    private readonly ICachingVersificationService _versificationService = _app.GetRequiredService<ICachingVersificationService>();

    [Theory]
    [InlineData(
        BsbBibleId,
        1001001001,
        true,
        "the BSB contains the given verse")]
    [InlineData(
        BsbBibleId,
        1041017021,
        false,
        "the BSB does not have the given verse")]
    [InlineData(
        BsbBibleId,
        1072001001,
        false,
        "the BSB does not have the given book")]
    [InlineData(
        EngVersificationSchemeBibleId,
        1001001001,
        true,
        "ENG contains the given verse")]
    [InlineData(
        EngVersificationSchemeBibleId,
        1041017021,
        true,
        "the BSB does not have the given verse")]
    [InlineData(
        EngVersificationSchemeBibleId,
        1072001001,
        true,
        "ENG contains the given verse")]
    public async Task IsValidVerseId_ValidArguments_Success(
        int bibleId,
        int verseId,
        bool expected,
        string because)
    {
        var result = await VersificationUtilities.IsValidVerseIdAsync(
            bibleId,
            verseId,
            _versificationService,
            CancellationToken.None);

        result.Should().Be(expected, because);
    }

    [Theory]
    [InlineData(
        BsbBibleId,
        1001001001,
        1001001001,
        new[] { 1001001001 },
        "a range of a single verse should return that verse")]
    [InlineData(
        BsbBibleId,
        1001001001,
        1001001003,
        new[] { 1001001001, 1001001002, 1001001003 },
        "a simple range within a single chapter should be correctly returned")]
    [InlineData(
        BsbBibleId,
        1041017020,
        1041017022,
        new[] { 1041017020, 1041017022 },
        "a range should not include excluded verse IDs for the given Bible")]
    [InlineData(
        BsbBibleId,
        1041017027,
        1041018002,
        new[] { 1041017027, 1041018001, 1041018002 },
        "a range spanning two chapters should be correctly returned")]
    [InlineData(
        BsbBibleId,
        1041028019,
        1042001002,
        new[] { 1041028019, 1041028020, 1042001001, 1042001002 },
        "a range spanning two books should be correctly returned")]
    public async Task ExpandVerseIdRangeAsync_ValidArguments_Success(
        int bibleId,
        int startVerseId,
        int endVerseId,
        IReadOnlyList<int> expectedVerseIds,
        string because)
    {
        var results = await VersificationUtilities.ExpandVerseIdRangeAsync(
            bibleId,
            startVerseId,
            endVerseId,
            _versificationService,
            CancellationToken.None);

        results.Should().Equal(expectedVerseIds, because);
    }

    [Theory]
    [InlineData(
        BsbBibleId,
        BsbBibleId,
        1001001001,
        1001001001,
        "the source and target Bibles are the same (with versification mappings) so the source and target verse IDs should be the same")]
    [InlineData(
        EngVersificationSchemeBibleId,
        BsbBibleId,
        1041017021,
        null,
        "the target (BSB) does not have the given verse")]
    [InlineData(
        BsbBibleId,
        RsbBibleId,
        1019010007,
        1019009028,
        "the RSB has a different versification than the BSB for the given verse")]
    [InlineData(
        RsbBibleId,
        BsbBibleId,
        1019009028,
        1019010007,
        "the RSB has a different versification than the BSB for the given verse")]
    [InlineData(
        RsbBibleId,
        BsbBibleId,
        1016007068,
        1016007067,
        "the RSB has a different versification than the BSB for the given verse with a non-matching base verse part")]
    [InlineData(
        EngVersificationSchemeBibleId,
        Ls1910BibleId,
        1004026001,
        1004026001,
        "the ENG and LS1910 have the same versification with matching base verse parts")]
    [InlineData(
        Ls1910BibleId,
        EngVersificationSchemeBibleId,
        1004026001,
        1004026001,
        "the ENG and LS1910 have the same versification with matching base verse parts")]
    public async Task ConvertVersification_ValidArguments_Success(
        int sourceBibleId,
        int targetBibleId,
        int sourceVerseId,
        int? expectedTargetVerseId,
        string because)
    {
        var results = await VersificationUtilities.ConvertVersificationAsync(
            sourceBibleId,
            sourceVerseId,
            targetBibleId,
            _versificationService,
            CancellationToken.None);

        if (expectedTargetVerseId != null)
        {
            results.Should().HaveCount(1);
            results[0].Should().Be(expectedTargetVerseId, because);
        }
        else
        {
            results.Should().BeEmpty(because);
        }
    }

    [Theory]
    [InlineData(
        RsbBibleId,
        BsbBibleId,
        1003014055,
        new[] { 1003014055, 1003014056 },
        "the RSB uses two different verse IDs for the BSB's single verse ID")]
    [InlineData(
        BsbBibleId,
        RsbBibleId,
        1016007067,
        new[] { 1016007067, 1016007068 },
        "the RSB has a three mappings for the BSB's non-mapping")]
    [InlineData(
        BsbBibleId,
        EngVersificationSchemeBibleId,
        1004026001,
        new[] { 1004026001 },
        "the ENG has a different versification than the BSB, the Bible verse part of 'b' is not loaded, and the base verse part of 'b' is ignored which results in mapping back to the same verse ID")]
    [InlineData(
        EngVersificationSchemeBibleId,
        BsbBibleId,
        1004026001,
        new[] { 1004026001 },
        "the ENG maps to two verse references in the BSB but one doesn't exist in the BSB so it is constrained to only a single verse")]
    public async Task ConvertVersification_ValidArguments_MultipleResultsSuccess(
        int sourceBibleId,
        int targetBibleId,
        int sourceVerseId,
        int[] expectedTargetVerseIds,
        string because)
    {
        var results = await VersificationUtilities.ConvertVersificationAsync(
            sourceBibleId,
            sourceVerseId,
            targetBibleId,
            _versificationService,
            CancellationToken.None);

        results.Should().BeEquivalentTo(expectedTargetVerseIds, because);
    }

    [Fact]
    public async Task ConvertVersificationRange_ValidArgumentsWithExclusionsInSource_Success()
    {
        var results = await VersificationUtilities.ConvertVersificationRangeAsync(
            BsbBibleId,
            1046016023,
            1046016025,
            IrvBibleId,
            _versificationService,
            CancellationToken.None);

        var expectedResults = new Dictionary<int, IReadOnlyList<int>>
            {
                [1046016023] = [1046016023],
                //[1046016024] = [1046016024], // The BSB doesn't have Rom. 16:24
                [1046016025] = [1046016025],
            }
            .AsReadOnly();

        results.Should().BeEquivalentTo(expectedResults, "source has exclusion");
    }

    [Fact]
    public async Task ConvertVersificationRange_ValidArgumentsWithExclusionInTarget_Success()
    {
        var results = await VersificationUtilities.ConvertVersificationRangeAsync(
            IrvBibleId,
            1046016023,
            1046016025,
            BsbBibleId,
            _versificationService,
            CancellationToken.None);

        var expectedResults = new Dictionary<int, IReadOnlyList<int>>
            {
                [1046016023] = [1046016023],
                [1046016024] = [], // The BSB doesn't have Rom. 16:24
                [1046016025] = [1046016025],
            }
            .AsReadOnly();

        results.Should().BeEquivalentTo(expectedResults, "target has exclusion");
    }
}