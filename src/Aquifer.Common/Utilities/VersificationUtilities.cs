using System.Collections.ObjectModel;
using Aquifer.Common.Services.Caching;

namespace Aquifer.Common.Utilities;

public static class VersificationUtilities
{
    public static async Task<ReadOnlyDictionary<int, int>> GetFromBibleToBibleVersificationMap(int fromBibleId,
        int targetBibleId,
        ICachingVersificationService versificationService, CancellationToken ct)
    {
        var fromBibleToBaseMapping = await versificationService.GetVersificationsByBibleIdAsync(fromBibleId, ct);
        var targetBibleToBaseMapping = await versificationService.GetVersificationsByBibleIdAsync(targetBibleId, ct);

        var merged = new Dictionary<int, int>();
        foreach (var fromMapping in fromBibleToBaseMapping)
        {
            var mappedBibleVerse = targetBibleToBaseMapping.FirstOrDefault(x => x.Value == fromMapping.Value).Key;
            merged[fromMapping.Key] = mappedBibleVerse;
        }

        return merged.AsReadOnly();
    }

    public static async Task<(int mappedStartVerse, int mappedEndVerse)> GetVersificationsForStartAndEndVerses(int startVerseId, int endVerseId, int fromBibleId, int targetBibleId, ICachingVersificationService versificationService, CancellationToken ct)
    {
        var cacheMap = await GetFromBibleToBibleVersificationMap(fromBibleId, targetBibleId, versificationService, ct);

        var mappedStartVerse = cacheMap.GetValueOrDefault(startVerseId, startVerseId);

        var mappedEndVerse = cacheMap.GetValueOrDefault(endVerseId, endVerseId);

        return (mappedStartVerse, mappedEndVerse);
    }
}