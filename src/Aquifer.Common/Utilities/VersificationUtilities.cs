using Aquifer.Common.Services.Caching;

namespace Aquifer.Common.Utilities;

public static class VersificationUtilities
{
    public static async Task<int> GetVersificationAsync(int sourceBibleVerseId, int sourceBibleId, int targetBibleId,
        ICachingVersificationService versificationService, CancellationToken ct)
    {
        var baseVerseIdBySourceBibleVerseIdMapping = await versificationService.GetBaseVerseIdByBibleVerseIdMapAsync(sourceBibleId, ct);
        var targetBibleVerseIdByBaseVerseIdMapping = await versificationService.GetBibleVerseIdByBaseVerseIdMapAsync(targetBibleId, ct);

        // The dictionary only contains mappings where the key is different from the value.
        // If the key verse ID is not present in the mapping (and is not in the exclusions list) then the value matches the key.
        var baseVerseId = baseVerseIdBySourceBibleVerseIdMapping.GetValueOrDefault(sourceBibleVerseId, sourceBibleVerseId);

        return targetBibleVerseIdByBaseVerseIdMapping.GetValueOrDefault(baseVerseId, baseVerseId);
    }
}