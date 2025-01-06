using Aquifer.Common.Services.Caching;

namespace Aquifer.Common.Utilities;

public static class VersificationUtilities
{
    public static async Task<(int mappedStartVerse, int mappedEndVerse)> GetVersificationsForStartAndEndVerses(int startVerseId, int endVerseId, int fromBibleId, int targetBibleId, ICachingVersificationService versificationService, CancellationToken ct)
    {
        var baseVerseIdByBibleVerseIdMapping = await versificationService.GetBaseVerseIdByBibleVerseIdMapAsync(fromBibleId, ct);
        var targetBibleVerseIdByBaseVerseIdMapping = await versificationService.GetBibleVerseIdByBaseVerseIdMapAsync(targetBibleId, ct);
        var exclusions = await versificationService.GetExclusionsByBibleIdAsync(targetBibleId, ct);

        var mappedStartVerse =
            targetBibleVerseIdByBaseVerseIdMapping.GetValueOrDefault(
                baseVerseIdByBibleVerseIdMapping.GetValueOrDefault(startVerseId, startVerseId), startVerseId);

        var mappedEndVerse =
            targetBibleVerseIdByBaseVerseIdMapping.GetValueOrDefault(
                baseVerseIdByBibleVerseIdMapping.GetValueOrDefault(endVerseId, endVerseId), endVerseId);

        return (mappedStartVerse, mappedEndVerse);
    }
}