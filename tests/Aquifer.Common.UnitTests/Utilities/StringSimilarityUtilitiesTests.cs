using Aquifer.Common.Utilities;

namespace Aquifer.Common.UnitTests.Utilities;

public sealed class StringSimilarityUtilitiesTests
{
    [Theory]
    [InlineData("This is longer than two words.", "This is longer than two words.", 17, 1.0)]
    [InlineData("This is longer than two words.", "This is longer than two words.", 5, 1.0)]
    [InlineData("This is longer than two words.", "This is longer .", 5, 0.5)]
    [InlineData("This is longer .", "This is longer than two words.", 5, 0.5)]
    [InlineData("Thisislongerthantwowords.", "Thisislonger.", 5, 0.47999999999999998)]
    public void StringSimilarity_ShouldReturnCorrectSimilarity(
        string firstString, string secondString, int limit, IComparable<double> expectedSimilarity)
    {
        var similarity = StringSimilarityUtilities.ComputeLevenshteinSimilarity(
            firstString, 
            secondString, 
            limit);
        
        Assert.Equal(expectedSimilarity, similarity);
    }
}