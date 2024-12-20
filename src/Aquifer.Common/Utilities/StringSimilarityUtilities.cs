namespace Aquifer.Common.Utilities;

public static class StringSimilarityUtilities
{
    public static double ComputeLevenshteinSimilarity(string textA, string textB, int limit = 15000)
    {
        var textASubstrings = GetSubstrings(textA, limit);
        var textBSubstrings = GetSubstrings(textB, limit);
        var distance = ComputeTotalStringDistance(textASubstrings, textBSubstrings);

        return 1 - ((double)distance / Math.Max(textA.Length, textB.Length));
    }
    
    public static int LevenshteinDistance(string textA, string textB)
    {
        var aLength = textA.Length;
        var bLength = textB.Length;
        var matrix = new int[bLength + 1];

        for (var j = 0; j <= bLength; j++)
        {
            matrix[j] = j;
        }

        for (var i = 1; i <= aLength; i++)
        {
            var prev = matrix[0];
            matrix[0] = i;
            for (var j = 1; j <= bLength; j++)
            {
                var cost = (textA[i - 1] == textB[j - 1]) ? 0 : 1;
                var temp = matrix[j];
                matrix[j] = Math.Min(
                    Math.Min(
                        prev + cost, 
                        matrix[j] + 1
                    ), 
                    matrix[j - 1] + 1
                );
                prev = temp;
            }
        }

        return matrix[bLength];
    }
    
    public static List<string> GetSubstrings(string text, int limit)
    {
        var result = new List<string>();
        
        while (text.Length > limit)
        {
            string? substring;
            
            if (text.LastIndexOf(' ', limit) == -1)
            { 
                substring = text[..limit];
                text = text[limit..];
            }
            else
            {
                substring = text[..text.LastIndexOf(' ', limit)];
                text = text[(text.LastIndexOf(' ', limit) + 1)..];
            }
            
            result.Add(substring);
        }
        result.Add(text);
        
        return result;
    }

    public static int ComputeTotalStringDistance(List<string> listA, List<string> listB)
    {
        var totalDistance = 0;
        List<string> longestList;
        List<string> shortestList;
        
        if (listA.Count > listB.Count)
        {
            longestList = listA;
            shortestList = listB;
        }
        else
        {
            longestList = listB;
            shortestList = listA;
        }
        
        // We have to account for lists that are not the same length.
        // Levenshtein distance is cumulative, so the distance of a string
        // compared to an empty string is the character count of that non-empty string 
        // i.e. "123" and "" is a distance of 3
        for (var i = 0; i < longestList.Count; i++)
        {
            if (i >= shortestList.Count)
            {
                totalDistance += LevenshteinDistance(longestList[i], string.Empty);
            }
            else
            {
                totalDistance += LevenshteinDistance(longestList[i], shortestList[i]);
            }
        }
        
        return totalDistance;
    }
}