using Aquifer.Common.Services.Caching;

namespace Aquifer.Common.Utilities;

public static class VersificationUtilities
{
    public static async Task<int> GetVersificationAsync(int verseId, int fromBibleId, int targetBibleId,
        ICachingVersificationService versificationService, CancellationToken ct)
    {
        var baseVerseIdByFromBibleVerseIdMapping = await versificationService.GetBaseVerseIdByBibleVerseIdMapAsync(fromBibleId, ct);
        var targetBibleVerseIdByBaseVerseIdMapping = await versificationService.GetBibleVerseIdByBaseVerseIdMapAsync(targetBibleId, ct);

        var baseVerseId = baseVerseIdByFromBibleVerseIdMapping.GetValueOrDefault(verseId, verseId);

        return targetBibleVerseIdByBaseVerseIdMapping.GetValueOrDefault(baseVerseId, baseVerseId);
    }
}