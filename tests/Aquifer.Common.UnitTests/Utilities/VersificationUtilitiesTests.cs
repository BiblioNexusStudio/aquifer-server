using System.Collections.ObjectModel;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;

namespace Aquifer.Common.UnitTests.Utilities;

public class VersificationUtilitiesTests
{
    public static TheoryData<int, int, Dictionary<int, int>> GetVersificationMappingData => new()
    {
        {
            1, 9, new Dictionary<int, int>
            {
                {
                    1001003001, 1001003003
                },
                {
                    1001003002, 1001003004
                },
                {
                    1001003003, 1001003005
                }
            }
        }
    };

    [Theory]
    [MemberData(nameof(GetVersificationMappingData))]
    public async Task GetVersificationFromBibleToBible_Success(int fromBibleId, int toBibleId, Dictionary<int, int> expectedMapping)
    {
        var mapping = await VersificationUtilities.GetFromBibleToBibleVersificationMap(fromBibleId, toBibleId,
            new MockCachingVersificationService(), CancellationToken.None);

        Assert.Equal(expectedMapping, mapping);
    }

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
        versificationMappings = new()
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

    private static readonly Dictionary<int, List<int>>
        exclusionMappings = new()
        {
            {
                1, [
                    1001004001,
                    1001004002
                ]
            },
            {
                9, [
                    1001004001,
                    1001004002
                ]
            }
        };

    public Task<ReadOnlyDictionary<int, int>>
        GetVersificationsByBibleIdAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(versificationMappings.GetValueOrDefault(bibleId)?.AsReadOnly() ?? new Dictionary<int, int>().AsReadOnly());
    }

    public Task<IReadOnlyList<int>> GetExclusionsByBibleIdAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyList<int>>(exclusionMappings.GetValueOrDefault(bibleId) ??
        [
        ]);
    }
}