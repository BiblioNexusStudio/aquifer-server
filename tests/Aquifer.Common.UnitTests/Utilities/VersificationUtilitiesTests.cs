using System.Collections.ObjectModel;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;

namespace Aquifer.Common.UnitTests.Utilities;

public class VersificationUtilitiesTests
{
    [Theory]
    [InlineData(1, 9, 1001003001, 1001003003, "Verse from Bible 1 has a different versification in Bible 9")]
    [InlineData(1, 9, 1001002029, 1001002029, "Verse from Bible 1 does not have a versification at all, so should be itself")]
    [InlineData(1, 8, 1001002029, 1001002029,
        "Verses should be the same because there are no versification mappings at all from target Bible")]
    [InlineData(1, 9, 1001003004, 1001003005,
        "Mapped verse should be base verse if there is no versification from the foreign Bible to Base")]
    public async Task GetVersificationStartAndEnd(int fromBible, int toBible, int verse, int expected, string because)
    {
        var result = await VersificationUtilities.GetVersificationAsync(verse,
            fromBible, toBible, new MockCachingVersificationService(), CancellationToken.None);

        result.Should().Be(expected, because);
    }
}

public class MockCachingVersificationService : ICachingVersificationService
{
    private static readonly Dictionary<int, Dictionary<int, int>>
        baseVerseByBibleIdMappings = new()
        {
            {
                1, new Dictionary<int, int>
                {
                    {
                         1001003001, 1001003002
                    },
                    {
                        1001003002, 1001003003
                    },
                    {
                        1001003003, 1001003004
                    },
                    {
                        1001003004, 1001003005
                    }
                }
            },
            {
                9, new Dictionary<int, int>
                {
                    {
                        1001003003, 1001003002
                    },
                    {
                        1001003004, 1001003003
                    },
                    {
                        1001003005, 1001003004
                    }
                }
            }
        };

    private static readonly Dictionary<int, ReadOnlySet<int>>
        exclusionMappings = new()
        {
            {
                1, new ReadOnlySet<int>(
                    new HashSet<int>([
                        1001004001,
                        1001004002
                    ])
                )
            },
            {
                9, new ReadOnlySet<int>(
                    new HashSet<int>([
                        1001004001,
                        1001004002
                    ]))
            }
        };

    public Task<ReadOnlyDictionary<int, int>>
        GetBaseVerseIdByBibleVerseIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(baseVerseByBibleIdMappings.GetValueOrDefault(bibleId)?.AsReadOnly() ??
                               new Dictionary<int, int>().AsReadOnly());
    }

    public async Task<ReadOnlyDictionary<int, int>> GetBibleVerseIdByBaseVerseIdMapAsync(int bibleId, CancellationToken cancellationToken)
    {
        var map = await GetBaseVerseIdByBibleVerseIdMapAsync(bibleId, cancellationToken);
        var invertedMap = map.ToDictionary(x => x.Value, x => x.Key).AsReadOnly();
        return invertedMap;
    }

    Task<ReadOnlySet<int>> ICachingVersificationService.GetExclusionsByBibleIdAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(exclusionMappings.GetValueOrDefault(bibleId, new ReadOnlySet<int>(new HashSet<int>())));
    }
}