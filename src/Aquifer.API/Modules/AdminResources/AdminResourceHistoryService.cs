using Aquifer.Data;
using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.AdminResources;

public interface IAdminResourceHistoryService
{
    Task AddStatusHistoryAsync(int resourceContentVersionId, ResourceContentStatus status, int changedByUserId, CancellationToken ct);

    Task AddAssignedUserHistoryAsync(ResourceContentVersionEntity contentVersionEntity,
        int? assignedUserId,
        int changedByUserId,
        CancellationToken ct);

    Task AddStatusHistoryAsync(ResourceContentVersionEntity contentVersionEntity,
        ResourceContentStatus status,
        int changedByUserId,
        CancellationToken ct);

    Task AddAssignedUserHistoryAsync(int resourceContentVersionId,
        int? assignedUserId,
        int changedByUserId,
        CancellationToken ct);
}

public class AdminResourceHistoryService(AquiferDbContext _dbContext) : IAdminResourceHistoryService
{
    public async Task AddStatusHistoryAsync(int resourceContentVersionId,
        ResourceContentStatus status,
        int changedByUserId,
        CancellationToken ct)
    {
        var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
        {
            ResourceContentVersionId = resourceContentVersionId,
            Status = status,
            ChangedByUserId = changedByUserId,
            Created = DateTime.UtcNow
        };

        await _dbContext.ResourceContentVersionStatusHistory.AddAsync(resourceContentVersionStatusHistory, ct);
    }

    public async Task AddStatusHistoryAsync(ResourceContentVersionEntity contentVersionEntity,
        ResourceContentStatus status,
        int changedByUserId,
        CancellationToken ct)
    {
        var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
        {
            ResourceContentVersion = contentVersionEntity,
            Status = status,
            ChangedByUserId = changedByUserId,
            Created = DateTime.UtcNow
        };

        await _dbContext.ResourceContentVersionStatusHistory.AddAsync(resourceContentVersionStatusHistory, ct);
    }

    public async Task AddAssignedUserHistoryAsync(ResourceContentVersionEntity contentVersionEntity,
        int? assignedUserId,
        int changedByUserId,
        CancellationToken ct)
    {
        var resourceContentVersionAssignedUserHistory = new ResourceContentVersionAssignedUserHistoryEntity
        {
            ResourceContentVersion = contentVersionEntity,
            AssignedUserId = assignedUserId,
            ChangedByUserId = changedByUserId,
            Created = DateTime.UtcNow
        };

        await _dbContext.ResourceContentVersionAssignedUserHistory.AddAsync(resourceContentVersionAssignedUserHistory, ct);
    }

    public async Task AddAssignedUserHistoryAsync(int resourceContentVersionId,
        int? assignedUserId,
        int changedByUserId,
        CancellationToken ct)
    {
        var resourceContentVersionAssignedUserHistory = new ResourceContentVersionAssignedUserHistoryEntity
        {
            ResourceContentVersionId = resourceContentVersionId,
            AssignedUserId = assignedUserId,
            ChangedByUserId = changedByUserId,
            Created = DateTime.UtcNow
        };

        await _dbContext.ResourceContentVersionAssignedUserHistory.AddAsync(resourceContentVersionAssignedUserHistory, ct);
    }
}