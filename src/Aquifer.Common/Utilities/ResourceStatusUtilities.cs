using Aquifer.Data.Enums;

namespace Aquifer.Common.Utilities;

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