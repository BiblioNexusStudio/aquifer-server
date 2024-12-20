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
    public async Task GetVersification_Success(int fromBibleId, int toBibleId, Dictionary<int, int> expectedMapping)
    {
        var mapping = await VersificationUtilities.GetFromBibleToBibleVersificationMap(fromBibleId, toBibleId,
            new MockCachingVersificationService(), CancellationToken.None);

        Assert.Equal(expectedMapping, mapping);
    }
}

public class MockCachingVersificationService : ICachingVersificationService
{
    private static readonly Dictionary<int, Dictionary<int, int>>
        mappings = new()
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

    public Task<ReadOnlyDictionary<int, int>>
        GetVersificationsByBibleIdAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(mappings.GetValueOrDefault(bibleId)?.AsReadOnly() ?? throw new KeyNotFoundException());
    }
}