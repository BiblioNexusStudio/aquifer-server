using Aquifer.Common.Services.Caching;

namespace Aquifer.Common.Utilities;

public static class VersificationUtilities
{
    public static async Task<int> GetVersificationAsync(int sourceBibleVerseId, int sourceFromBibleId, int targetBibleId,
        ICachingVersificationService versificationService, CancellationToken ct)
    {
        var baseVerseIdByFromBibleVerseIdMapping = await versificationService.GetBaseVerseIdByBibleVerseIdMapAsync(sourceFromBibleId, ct);
        var targetBibleVerseIdByBaseVerseIdMapping = await versificationService.GetBibleVerseIdByBaseVerseIdMapAsync(targetBibleId, ct);

        var baseVerseId = baseVerseIdByFromBibleVerseIdMapping.GetValueOrDefault(sourceBibleVerseId, sourceBibleVerseId);

        return targetBibleVerseIdByBaseVerseIdMapping.GetValueOrDefault(baseVerseId, baseVerseId);
    }
}