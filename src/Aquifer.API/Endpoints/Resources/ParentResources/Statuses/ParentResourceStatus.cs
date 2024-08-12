namespace Aquifer.API.Endpoints.Resources.ParentResources.Statuses;

public static class ResourceStatusUtilities
{

    public static ParentResourceStatus GetStatus(int totalCount, int totalLanguageCount, DateTime? lastPublished)
    {
        if (totalLanguageCount == 0)
        {
            return ParentResourceStatus.ComingSoon;
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
    Complete = 1,
    RecentlyCompleted = 2,
    Partial = 3,
    RecentlyUpdated = 4,
    ComingSoon = 5
}