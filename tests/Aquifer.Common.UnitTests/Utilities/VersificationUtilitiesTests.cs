using System.Collections.ObjectModel;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;

namespace Aquifer.Common.UnitTests.Utilities;

public class VersificationUtilitiesTests
{
    [Theory]
    [InlineData(1, 9, 1001003001, 1001003003, 1001003003,
        1001003005)]
    [InlineData(1, 9, 1001002029, 1001003003, 1001002029,
        1001003005)]
    [InlineData(1, 9, 1001002029, 1001002032, 1001002029,
        1001002032)]
    [InlineData(1, 8, 1001002029, 1001002032, 1001002029,
        1001002032)]
    public async Task GetVersificationStartAndEnd(int fromBible, int toBible, int inputStartVerse, int inputEndVerse,
        int expectedStart, int expectedEnd)
    {
        var (startVerse, endVerse) = await VersificationUtilities.GetVersificationsForStartAndEndVerses(inputStartVerse, inputEndVerse,
            fromBible, toBible, new MockCachingVersificationService(), CancellationToken.None);

        Assert.Equal(expectedStart, startVerse);
        Assert.Equal(expectedEnd, endVerse);
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