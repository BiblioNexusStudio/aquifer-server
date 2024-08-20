namespace Aquifer.API.Endpoints.Marketing.ParentResourceStatuses;

public static class ParentResourceStatusHelpers
{
    public static ParentResourceStatus GetStatus(int totalCount, int totalLanguageCount, DateTime? lastPublished)
    {
        if (totalLanguageCount == 0)
        {
            return totalCount == 0 ? ParentResourceStatus.NoStatus : ParentResourceStatus.ComingSoon;
        }

        var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
        if (totalLanguageCount < totalCount)
        {
            return lastPublished >= thirtyDaysAgo ? ParentResourceStatus.RecentlyUpdated : ParentResourceStatus.Partial;
        }

        if (totalLanguageCount == totalCount && lastPublished >= thirtyDaysAgo)
        {
            return ParentResourceStatus.RecentlyCompleted;
        }

        return ParentResourceStatus.Complete;
    }
}

public enum ParentResourceStatus
{
    NoStatus = 0,
    Complete = 1,
    RecentlyCompleted = 2,
    Partial = 3,
    RecentlyUpdated = 4,
    ComingSoon = 5
}