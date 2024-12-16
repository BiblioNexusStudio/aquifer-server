using Aquifer.Data;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Common.Services.Caching;

public interface ICachingVersificationService
{
    Task<int> GetVersificationMapping(int bibleId, int verseId, CancellationToken ct);

}

public sealed class CachingVersificationService(AquiferDbContext _dbContext) : ICachingVersificationService
{
    private const string VersificationMappingsCacheKey = $"{nameof(CachingVersificationService)}VersificationMappings";
    private const string VersificationBiblesCacheKey = $"{nameof(CachingVersificationService)}VersificationBibles";
    // private static readonly TimeSpan s_cacheLifetime = TimeSpan.FromMinutes(30);

    public async Task<int> GetVersificationMapping(int bibleId, int verseId, CancellationToken ct)
    {
        var englishMapping =
            await _dbContext.VersificationMappings.FirstOrDefaultAsync(vm => vm.BibleId == 1 && vm.BibleVerseId == verseId, ct);

        if (englishMapping == null)
        {
            return verseId;
        }

        var bibleMapping =
            await _dbContext.VersificationMappings.FirstOrDefaultAsync(
                vm => vm.BibleId == bibleId && vm.BaseVerseId == englishMapping.BaseVerseId, ct);

        return bibleMapping?.BibleVerseId ?? verseId;
    }
}