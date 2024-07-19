using Aquifer.Data.Enums;

namespace Aquifer.Common.Utilities;

public static class AlignmentUtilities
{
    public static long LowerBoundOfChapter(BookId bookId, int chapter)
    {
        return (((int)bookId - 1) * 10000000000) + (chapter * 10000000);
    }

    public static long UpperBoundOfChapter(BookId bookId, int chapter)
    {
        return (((int)bookId - 1) * 10000000000) + ((chapter + 1) * 10000000) - 1;
    }
}