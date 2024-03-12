using Aquifer.API.Common.Dtos;
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

    Task AddSnapshotHistoryAsync(int contentVersionId,
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
        await AddSnapshotHistoryAsync(contentVersionEntity, ct);

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
        await AddSnapshotHistoryAsync(resourceContentVersionId, ct);

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
        var resourceContentVersionSnapshot = new ResourceContentVersionSnapshotEntity
        {
            ResourceContentVersion = contentVersionEntity,
            Content = contentVersionEntity.Content,
            DisplayName = contentVersionEntity.DisplayName,
            WordCount = contentVersionEntity.WordCount,
            UserId = contentVersionEntity.AssignedUserId,
            Status = ResourceContentStatus.New,
            Created = DateTime.UtcNow
        };

        await _dbContext.ResourceContentVersionSnapshots.AddAsync(resourceContentVersionSnapshot, ct);
    }

    public async Task AddSnapshotHistoryAsync(int contentVersionId,
        CancellationToken ct)
    {
        var resourceStatusSnapshot = await _dbContext.ResourceContentVersions
            .Where(rcv => rcv.Id == contentVersionId)
            .Select(rcv => new ResourceStatusSnapshotDto
            {
                ResourceContentVersion = rcv,
                Status = rcv.ResourceContent.Status,
                Snapshot = rcv.ResourceContentVersionSnapshots
                    .OrderByDescending(rcvs => rcvs.Created)
                    .FirstOrDefault()
            })
            .FirstAsync(ct);

        if (resourceStatusSnapshot.Snapshot?.Content != resourceStatusSnapshot.ResourceContentVersion.Content)
        {
            var resourceContentVersionSnapshot = new ResourceContentVersionSnapshotEntity
            {
                ResourceContentVersionId = resourceStatusSnapshot.ResourceContentVersion.Id,
                Content = resourceStatusSnapshot.ResourceContentVersion.Content,
                DisplayName = resourceStatusSnapshot.ResourceContentVersion.DisplayName,
                WordCount = resourceStatusSnapshot.ResourceContentVersion.WordCount,
                UserId = resourceStatusSnapshot.ResourceContentVersion.AssignedUserId,
                Status = resourceStatusSnapshot.Status,
                Created = DateTime.UtcNow
            };

            await _dbContext.ResourceContentVersionSnapshots.AddAsync(resourceContentVersionSnapshot, ct);
        }
    }
}