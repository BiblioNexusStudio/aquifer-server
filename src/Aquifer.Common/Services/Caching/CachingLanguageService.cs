using System.Collections.ObjectModel;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Aquifer.Common.Services.Caching;

public interface ICachingLanguageService
{
    Task<Language?> GetLanguageAsync(int languageId, CancellationToken ct);
    Task<ReadOnlyDictionary<int, Language>> GetLanguageByIdMapAsync(CancellationToken ct);
    Task<string?> GetLanguageCodeAsync(int languageId, CancellationToken ct);
    Task<ReadOnlyDictionary<int, string>> GetLanguageCodeByIdMapAsync(CancellationToken ct);
    Task<int?> GetLanguageIdAsync(string languageCode, CancellationToken ct);
    Task<ReadOnlyDictionary<string, int>> GetLanguageIdByCodeMapAsync(CancellationToken ct);

    Task<(bool IsValidLanguageId, bool IsValidLanguageCode, int? ValidLanguageId)>
        ValidateLanguageIdOrCodeAsync(int? languageId, string? languageCode, bool shouldRequireInput, CancellationToken ct);

    Task<
        (IReadOnlyList<int> InvalidLanguageIds,
        IReadOnlyList<string> InvalidLanguageCodes,
        IReadOnlyList<int>? ValidLanguageIds)>
        ValidateLanguageIdsOrCodesAsync(
            IReadOnlyList<int>? languageIds,
            IReadOnlyList<string>? languageCodes,
            bool shouldRequireInput,
            CancellationToken ct);
}

public record Language(
    int Id,
    string ISO6393Code,
    string EnglishDisplay,
    string DisplayName,
    bool Enabled,
    ScriptDirection ScriptDirection);

public sealed class CachingLanguageService(AquiferDbContext _dbContext, IMemoryCache _memoryCache) : ICachingLanguageService
{
    private const string LanguageDictionariesCacheKey = $"{nameof(CachingLanguageService)}:LanguageDictionaries";
    private static readonly TimeSpan s_cacheLifetime = TimeSpan.FromMinutes(30);

    public async Task<Language?> GetLanguageAsync(int languageId, CancellationToken ct)
    {
        return (await GetLanguageByIdMapAsync(ct))
            .GetValueOrDefault(languageId);
    }

    public async Task<ReadOnlyDictionary<int, Language>> GetLanguageByIdMapAsync(CancellationToken ct)
    {
        return (await GetDictionariesFromCacheAsync(ct))
            .LanguageByIdMap;
    }

    public async Task<string?> GetLanguageCodeAsync(int languageId, CancellationToken ct)
    {
        return (await GetLanguageCodeByIdMapAsync(ct))
            .GetValueOrDefault(languageId);
    }

    public async Task<ReadOnlyDictionary<int, string>> GetLanguageCodeByIdMapAsync(CancellationToken ct)
    {
        return (await GetDictionariesFromCacheAsync(ct))
            .LanguageCodeByIdMap;
    }

    public async Task<int?> GetLanguageIdAsync(string languageCode, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(languageCode);

        return (await GetLanguageIdByCodeMapAsync(ct))
            .GetValueOrNull(languageCode);
    }

    public async Task<ReadOnlyDictionary<string, int>> GetLanguageIdByCodeMapAsync(CancellationToken ct)
    {
        return (await GetDictionariesFromCacheAsync(ct))
            .LanguageIdByCodeMap;
    }

    private async Task<(ReadOnlyDictionary<int, Language> LanguageByIdMap, ReadOnlyDictionary<int, string> LanguageCodeByIdMap, ReadOnlyDictionary<string, int> LanguageIdByCodeMap)> GetDictionariesFromCacheAsync(CancellationToken ct)
    {
        return await _memoryCache.GetOrCreateAsync(
            LanguageDictionariesCacheKey,
            async cacheEntry =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = s_cacheLifetime;

                var languageByIdMap = (await _dbContext.Languages
                        .ToListAsync(ct))
                    .ToDictionary(
                        l => l.Id,
                        l => new Language(l.Id, l.ISO6393Code, l.EnglishDisplay, l.DisplayName, l.Enabled, l.ScriptDirection))
                    .AsReadOnly();

                var languageCodeByIdMap = languageByIdMap
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ISO6393Code)
                    .AsReadOnly();

                var languageIdByCodeMap = languageCodeByIdMap
                    .ToDictionary(kvp => kvp.Value, kvp => kvp.Key, StringComparer.OrdinalIgnoreCase)
                    .AsReadOnly();

                return (languageByIdMap, languageCodeByIdMap, languageIdByCodeMap);
            });
    }

    public async Task<(bool IsValidLanguageId, bool IsValidLanguageCode, int? ValidLanguageId)>
        ValidateLanguageIdOrCodeAsync(int? languageId, string? languageCode, bool shouldRequireInput, CancellationToken ct)
    {
        if (languageId is not null && languageCode is not null)
        {
            throw new ArgumentException($"Only one of {nameof(languageId)} or {nameof(languageCode)} may be provided.");
        }

        if (languageId is not null)
        {
            return (
                IsValidLanguageId: await GetLanguageCodeAsync(languageId.Value, ct) is not null,
                IsValidLanguageCode: true,
                languageId.Value);
        }

        if (languageCode is not null)
        {
            var languageIdForCode = await GetLanguageIdAsync(languageCode, ct);
            return (
                IsValidLanguageId: true,
                IsValidLanguageCode: languageIdForCode is not null,
                languageIdForCode.GetValueOrDefault());
        }

        if (!shouldRequireInput)
        {
            return (
                IsValidLanguageId: true,
                IsValidLanguageCode: true,
                ValidLanguageId: null);
        }

        throw new ArgumentException($"One of {nameof(languageId)} or {nameof(languageCode)} must be non-null.");
    }

    public async Task<
            (IReadOnlyList<int> InvalidLanguageIds,
            IReadOnlyList<string> InvalidLanguageCodes,
            IReadOnlyList<int>? ValidLanguageIds)>
        ValidateLanguageIdsOrCodesAsync(
            IReadOnlyList<int>? languageIds,
            IReadOnlyList<string>? languageCodes,
            bool shouldRequireInput,
            CancellationToken ct)
    {
        if (languageIds is not null && languageCodes is not null)
        {
            throw new ArgumentException($"Only one of {nameof(languageIds)} or {nameof(languageCodes)} may be provided.");
        }

        if (languageIds is not null or [])
        {
            var languageCodeByIdMap = await GetLanguageCodeByIdMapAsync(ct);

            var invalidLanguageIds = new List<int>();
            var validLanguageIds = new List<int>();
            foreach (var languageId in languageIds)
            {
                if (!languageCodeByIdMap.ContainsKey(languageId))
                {
                    invalidLanguageIds.Add(languageId);
                }
                else
                {
                    validLanguageIds.Add(languageId);
                }
            }

            return (invalidLanguageIds, InvalidLanguageCodes: [], validLanguageIds);
        }

        if (languageCodes is not null or [])
        {
            var languageIdByCodeMap = await GetLanguageIdByCodeMapAsync(ct);

            var invalidLanguageCodes = new List<string>();
            var validLanguageIds = new List<int>();
            foreach (var languageCode in languageCodes)
            {
                if (!languageIdByCodeMap.TryGetValue(languageCode, out var id))
                {
                    invalidLanguageCodes.Add(languageCode);
                }
                else
                {
                    validLanguageIds.Add(id);
                }
            }

            return (InvalidLanguageIds: [], invalidLanguageCodes, validLanguageIds);
        }

        if (!shouldRequireInput)
        {
            return (InvalidLanguageIds: [], InvalidLanguageCodes: [], ValidLanguageIds: null);
        }

        throw new ArgumentException($"One of {nameof(languageIds)} or {nameof(languageCodes)} must be non-null and non-empty.");
    }
}