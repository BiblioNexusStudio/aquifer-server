namespace Aquifer.API.Utilities;

public static class ReportUtilities
{
    public static List<DateTime> GetLastMonths(int goBackAmount)
    {
        var currentDate = DateTime.UtcNow;
        return Enumerable.Range(0, goBackAmount)
            .Select(i => new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-i))
            .ToList();
    }
}