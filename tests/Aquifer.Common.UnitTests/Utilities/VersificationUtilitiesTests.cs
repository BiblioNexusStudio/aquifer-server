using System.Collections.ObjectModel;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;

namespace Aquifer.Common.UnitTests.Utilities;

public class VersificationUtilitiesTests
{
    public static TheoryData<int, int, Dictionary<(int, char?), (int, char?)>> GetVersificationMappingData => new()
    {
        {
            1, 9, new Dictionary<(int, char?), (int, char?)>
            {
                {
                    (1001003001, null), (1001003003, null)
                },
                {
                    (1001003002, null), (1001003004, null)
                },
                {
                    (1001003003, null), (1001003005, null)
                }
            }
        }
    };

    [Theory]
    [MemberData(nameof(GetVersificationMappingData))]
    public async Task GetVersification_Success(int fromBibleId, int toBibleId, Dictionary<(int, char?), (int, char?)> expectedMapping)
    {
        var mapping = await VersificationUtilities.GetFromBibleToBibleVersificationMap(fromBibleId, toBibleId,
            new MockCachingVersificationService(), CancellationToken.None);

        Assert.Equal(expectedMapping, mapping);
    }
}

public class MockCachingVersificationService : ICachingVersificationService
{
    private static readonly Dictionary<int, Dictionary<(int bibleVerseId, char? bibleVersePart), (int baseVerseId, char? baseVersePart)>>
        mappings = new()
        {
            {
                1, new Dictionary<(int bibleVerseId, char? bibleVersePart), (int baseVerseId, char? baseVersePart)>
                {
                    {
                        (1001003001, null), (1001003002, null)
                    },
                    {
                        (1001003002, null), (1001003003, null)
                    },
                    {
                        (1001003003, null), (1001003004, null)
                    }
                }
            },
            {
                9, new Dictionary<(int bibleVerseId, char? bibleVersePart), (int baseVerseId, char? baseVersePart)>
                {
                    {
                        (1001003003, null), (1001003002, null)
                    },
                    {
                        (1001003004, null), (1001003003, null)
                    },
                    {
                        (1001003005, null), (1001003004, null)
                    }
                }
            }
        };

    public Task<ReadOnlyDictionary<(int bibleVerseId, char? bibleVersePart), (int baseVerseId, char? baseVersePart)>>
        GetVersificationsByBibleIdAsync(int bibleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(mappings.GetValueOrDefault(bibleId)?.AsReadOnly() ?? throw new KeyNotFoundException());
    }
}