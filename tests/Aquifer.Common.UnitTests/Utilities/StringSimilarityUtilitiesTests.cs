using Aquifer.Common.Utilities;

namespace Aquifer.Common.UnitTests.Utilities;

public sealed class StringSimilarityUtilitiesTests
{
    [Theory]
    [ClassData(typeof(StringDistanceTestData))]
    public void GivenTwoStringsGetLevenshteinDistance_ShouldReturnCorrectDistance( StringPair strings, int expectedDistance)
    {
        var distance = StringSimilarityUtilities.LevenshteinDistance(strings.First, strings.Second);
        
        Assert.Equal(expectedDistance, distance);
    }

    [Theory]
    [ClassData(typeof(SubStringSplitTestData))]
    public void LongStringsShouldBeSplitOnLimit_ShouldReturnCorrectSubstrings(SubStringLimit strings, List<string> expectedSubstrings)
    {
        var subStrings = StringSimilarityUtilities.GetSubstrings(strings.First, strings.Limit);
        
        Assert.Equal(expectedSubstrings, subStrings);
    }
    
    [Theory]
    [ClassData(typeof(TotalStingDistanceTestData))]
    public void SplitStringDistanceShouldEqualStringDistance_ShouldReturnCorrectDistance(
        List<string> substringsListA, 
        List<string> substringsListB,
        int expectedDistance)
    {
        var totalDistance = StringSimilarityUtilities.ComputeTotalStringDistance(substringsListA, substringsListB);
        
        Assert.Equal(expectedDistance, totalDistance);
    }

    [Theory]
    [ClassData(typeof(StringSimilarityTestData))]
    public void StringSimilarity_ShouldReturnCorrectSimilarityAndExecutionTimeMs(
        StringPair strings, IComparable<double> expectedSimilarity)
    {
        var similarity = StringSimilarityUtilities.ComputeLevenshteinSimilarity(
            strings.First, 
            strings.Second, 
            strings.Limit);
        
        Assert.Equal(expectedSimilarity, similarity);
    }
}

public class StringPair
{
    public required string First { get; set; }
    public required string Second { get; set; }
    public int Limit { get; set; }
}

public class StringDistanceTestData : TheoryData<StringPair, int>
{
    public StringDistanceTestData()
    {
        Add( new StringPair { First = "abc", Second = "abc" }, 0);
        Add( new StringPair { First = "abc", Second = "abd" }, 1);
        Add( new StringPair { First = "This big car", Second = "this big cat" }, 2);
        Add( new StringPair { First = "This is longer than two", Second = "This is longer t" }, 7);
    }
}

public class SubStringLimit
{
    public required string First { get; set; }
    public required int Limit { get; set; }
}

public class SubStringSplitTestData : TheoryData<SubStringLimit, List<string>>
{
    public SubStringSplitTestData()
    {
        Add( 
            new SubStringLimit { First = "This is longer than two words.", Limit = 50 },
            ["This is longer than two words."]
        );
        Add( 
            new SubStringLimit { First = "This is longer than two words.", Limit = 20 },
            ["This is longer than", "two words."]
        );
        Add( 
            new SubStringLimit { First = "This is longer than two words.", Limit = 17 },
            ["This is longer", "than two words."]
        );
        Add( 
            new SubStringLimit { First = "This is longer than two words.", Limit = 5 },
            ["This", "is", "longe", "r", "than", "two", "words", "."]
        );
    }
}

public class TotalStingDistanceTestData : TheoryData<List<string>, List<string>, int>
{
    public TotalStingDistanceTestData()
    {
        Add( 
            ["This is longer than two words."],
            ["This is longer than two words."],
            0
        );
        Add( 
            ["This is longer", "than two words."],
            ["This is longer", "than two words."],
            0
        );
        Add( 
            ["This is", "longer than two words."],
            ["This is", "longer than two words"],
            1
        );
        Add( 
            ["This is", "longer than", "two words."],
            ["This is", "longer than", "two"],
            7
        );
    }
}

public class StringSimilarityTestData : TheoryData<StringPair, IComparable<double>>
{
    public StringSimilarityTestData()
    {
        Add(
            new StringPair
            {
                First = "This is longer than two words.",
                Second = "This is longer than two words.",
                Limit = 17
            },
            1.0
        );
        Add(
            new StringPair
            {
                First = "This is longer than two words.",
                Second = "This is longer than two words.",
                Limit = 5
            },
            1.0
        );
        Add(
            new StringPair
            {
                First = "This is longer than two words.",
                Second = "This is longer .",
                Limit = 5
            },
            0.5
        );
    }
}