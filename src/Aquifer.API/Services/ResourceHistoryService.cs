using Aquifer.Data;
using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Services;

public interface IResourceHistoryService
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

    Task AddSnapshotHistoryAsync(ResourceContentVersionEntity contentVersionEntity,
        CancellationToken ct);
}

public class ResourceHistoryService(AquiferDbContext _dbContext) : IResourceHistoryService
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

    public async Task AddSnapshotHistoryAsync(ResourceContentVersionEntity contentVersionEntity,
        CancellationToken ct)
    {
        var currentSnapshot = await _dbContext.ResourceContentVersionSnapshots
            .Where(rcvs => rcvs.ResourceContentVersionId == contentVersionEntity.Id)
            .OrderByDescending(rcvs => rcvs.Created)
            .FirstOrDefaultAsync(ct);

        var resourceContent = await _dbContext.ResourceContents
            .Where(rc => rc.Id == contentVersionEntity.ResourceContentId)
            .FirstOrDefaultAsync(ct);

        if (currentSnapshot is not null && resourceContent is not null && currentSnapshot.Content != contentVersionEntity.Content)
        {
            var resourceContentVersionSnapshot = new ResourceContentVersionSnapshotEntity
            {
                ResourceContentVersionId = contentVersionEntity.Id,
                Content = contentVersionEntity.Content,
                DisplayName = contentVersionEntity.DisplayName,
                WordCount = contentVersionEntity.WordCount,
                UserId = contentVersionEntity.AssignedUserId,
                Status = resourceContent.Status,
                Created = DateTime.UtcNow
            };

            await _dbContext.ResourceContentVersionSnapshots.AddAsync(resourceContentVersionSnapshot, ct);
        }
    }
}