using System.Collections.ObjectModel;
using Aquifer.Common.Services.Caching;

namespace Aquifer.Common.Utilities;

public static class VersificationUtilities
{
    public static async Task<ReadOnlyDictionary<(int, char?), (int, char?)>> GetFromBibleToBibleVersificationMap(int fromBibleId,
        int toBibleId,
        ICachingVersificationService versificationService, CancellationToken ct)
    {
        var fromBibleToBaseMapping = await versificationService.GetVersificationsByBibleIdAsync(fromBibleId, ct);
        var toBibleToBaseMapping = await versificationService.GetVersificationsByBibleIdAsync(toBibleId, ct);

        var merged = new Dictionary<(int, char?), (int, char?)>();
        foreach (var englishMapping in fromBibleToBaseMapping)
        {
            var mappedBibleVerse = toBibleToBaseMapping.FirstOrDefault(x => x.Value == englishMapping.Value).Key;
            merged[englishMapping.Key] = mappedBibleVerse;
        }

        return merged.AsReadOnly();
    }
}