using Aquifer.Data;
using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.AdminResources;

public interface IAdminResourceHistoryService
{
    Task AddStatusHistoryAsync(int resourceContentVersionId, ResourceContentStatus status, int changedByUserId);

    Task AddAssignedUserHistoryAsync(ResourceContentVersionEntity contentVersionEntity,
        int? assignedUserId,
        int changedByUserId);

    Task AddStatusHistoryAsync(ResourceContentVersionEntity contentVersionEntity,
        ResourceContentStatus status,
        int changedByUserId);
}

public class AdminResourceHistoryService(AquiferDbContext _dbContext) : IAdminResourceHistoryService
{
    public async Task AddStatusHistoryAsync(int resourceContentVersionId,
        ResourceContentStatus status,
        int changedByUserId)
    {
        var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
        {
            ResourceContentVersionId = resourceContentVersionId,
            Status = status,
            ChangedByUserId = changedByUserId,
            Created = DateTime.UtcNow
        };

        await _dbContext.ResourceContentVersionStatusHistory.AddAsync(resourceContentVersionStatusHistory);
    }

    public async Task AddStatusHistoryAsync(ResourceContentVersionEntity contentVersionEntity,
        ResourceContentStatus status,
        int changedByUserId)
    {
        var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
        {
            ResourceContentVersion = contentVersionEntity,
            Status = status,
            ChangedByUserId = changedByUserId,
            Created = DateTime.UtcNow
        };

        await _dbContext.ResourceContentVersionStatusHistory.AddAsync(resourceContentVersionStatusHistory);
    }

    public async Task AddAssignedUserHistoryAsync(ResourceContentVersionEntity contentVersionEntity,
        int? assignedUserId,
        int changedByUserId)
    {
        var resourceContentVersionAssignedUserHistory = new ResourceContentVersionAssignedUserHistoryEntity
        {
            ResourceContentVersion = contentVersionEntity,
            AssignedUserId = assignedUserId,
            ChangedByUserId = changedByUserId,
            Created = DateTime.UtcNow
        };

        await _dbContext.ResourceContentVersionAssignedUserHistory.AddAsync(resourceContentVersionAssignedUserHistory);
    }
}