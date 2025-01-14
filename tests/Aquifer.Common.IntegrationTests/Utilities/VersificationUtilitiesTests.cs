using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;

namespace Aquifer.Common.IntegrationTests.Utilities;

public sealed class VersificationUtilitiesTests(App _app) : TestBase<App>
{
    private readonly ICachingVersificationService _versificationService = _app.GetRequiredService<ICachingVersificationService>();

    private const int _engVersificationSchemeBibleId = 0;
    private const int _bsbBibleId = 1;
    private const int _ls1910BibleId = 6;
    private const int _rsbBibleId = 9;
    private const int _gltBibleId = 10;

    [Theory]
    [InlineData(_bsbBibleId, 1001001001, true,
        "the BSB contains the given verse")]
    [InlineData(_bsbBibleId, 1041017021, false,
        "the BSB does not have the given verse")]
    [InlineData(_bsbBibleId, 1072001001, false,
        "the BSB does not have the given book")]
    [InlineData(_engVersificationSchemeBibleId, 1001001001, true,
        "ENG contains the given verse")]
    [InlineData(_engVersificationSchemeBibleId, 1041017021, true,
        "the BSB does not have the given verse")]
    [InlineData(_engVersificationSchemeBibleId, 1072001001, true,
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
    [InlineData(_bsbBibleId, 1001001001, 1001001001, new[] { 1001001001 },
        "a range of a single verse should return that verse")]
    [InlineData(_bsbBibleId, 1001001001, 1001001003, new[] { 1001001001, 1001001002, 1001001003 },
        "a simple range within a single chapter should be correctly returned")]
    [InlineData(_bsbBibleId, 1041017020, 1041017022, new[] { 1041017020, 1041017022 },
        "a range should not include excluded verse IDs for the given Bible")]
    [InlineData(_bsbBibleId, 1041017027, 1041018002, new[] { 1041017027, 1041018001, 1041018002 },
        "a range spanning two chapters should be correctly returned")]
    [InlineData(_bsbBibleId, 1041028019, 1042001002, new[] { 1041028019, 1041028020, 1042001001, 1042001002 },
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
    [InlineData(_bsbBibleId, _bsbBibleId, 1001001001, 1001001001,
        "the source and target Bibles are the same (with versification mappings) so the source and target verse IDs should be the same")]
    [InlineData(_engVersificationSchemeBibleId, _bsbBibleId, 1041017021, null,
        "the target (BSB) does not have the given verse")]
    [InlineData(_bsbBibleId, _rsbBibleId, 1001003001, 1001003003,
        "the RSB has a different versification than the BSB for the given verse")]
    [InlineData(_rsbBibleId, _bsbBibleId, 1001003003, 1001003001,
        "the RSB has a different versification than the BSB for the given verse")]
    [InlineData(_bsbBibleId, _rsbBibleId, 1016007067, 1016007068,
        "the RSB has a different versification than the BSB for the given verse with an ignored optional base verse part")]
    [InlineData(_rsbBibleId, _bsbBibleId, 1016007068, 1016007067,
        "the RSB has a different versification than the BSB for the given verse with a non-matching base verse part")]
    [InlineData(_engVersificationSchemeBibleId, _ls1910BibleId, 1004026001, 1004026001,
        "the ENG and LS1910 have the same versification with matching base verse parts")]
    [InlineData(_ls1910BibleId, _engVersificationSchemeBibleId, 1004026001, 1004026001,
        "the ENG and LS1910 have the same versification with matching base verse parts")]
    [InlineData(_bsbBibleId, _engVersificationSchemeBibleId, 1004026001, 1004026001,
        "the ENG has a different versification than the BSB, the Bible verse part of 'b' is not loaded, and the base verse part of 'b' is ignored which results in mapping back to the same verse ID")]
    [InlineData(_engVersificationSchemeBibleId, _bsbBibleId, 1004026001, 1004026001,
        "the ENG has a different versification than the BSB and the base verse part of 'b' is ignored which results in mapping to the same verse ID")]
    public async Task ConvertVersification_ValidArguments_Success(
        int sourceBibleId,
        int targetBibleId,
        int sourceVerseId,
        int? expectedTargetVerseId,
        string because)
    {
        var result = await VersificationUtilities.ConvertVersificationAsync(
            sourceBibleId,
            sourceVerseId,
            targetBibleId,
            _versificationService,
            CancellationToken.None);

        result.Should().Be(expectedTargetVerseId, because);
    }

    [Fact]
    public async Task ConvertVersificationRange_ValidArgumentsWithExclusionInSourceAndTarget_Success()
    {
        var results = await VersificationUtilities.ConvertVersificationRangeAsync(
            sourceBibleId: _bsbBibleId,
            1046016023,
            1046016027,
            targetBibleId: _gltBibleId,
            _versificationService,
            CancellationToken.None);

        var expectedResults = new Dictionary<int, int?>
            {
                [1046016023] = 1046016023,
                //[1046016024] = 1046016024, // The BSB doesn't have Rom. 16:24
                [1046016025] = 1046016025,
                [1046016026] = null, // The GLT doesn't have Rom. 16:26 or 16:27
                [1046016027] = null,
            }
            .AsReadOnly();

        results.Should().Equal(expectedResults, "source and target both have exclusions");
    }
}