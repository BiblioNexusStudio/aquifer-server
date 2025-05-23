﻿using Aquifer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Services;

public interface IResourceHistoryService
{
    Task AddAssignedUserHistoryAsync(
        ResourceContentVersionEntity contentVersionEntity,
        int? assignedUserId,
        int changedByUserId,
        CancellationToken ct);

    Task AddStatusHistoryAsync(
        ResourceContentVersionEntity contentVersionEntity,
        ResourceContentStatus status,
        int changedByUserId,
        CancellationToken ct);

    Task AddSnapshotHistoryAsync(
        ResourceContentVersionEntity contentVersionEntity,
        int? oldUserId,
        ResourceContentStatus oldStatus,
        CancellationToken ct);

    Task AddSnapshotHistoryAsync(
        ResourceContentVersionEntity contentVersionEntity,
        string content,
        string displayName,
        int? wordCount,
        int? oldUserId,
        ResourceContentStatus oldStatus,
        CancellationToken ct);
}

public class ResourceHistoryService(AquiferDbContext _dbContext) : IResourceHistoryService
{
    public async Task AddStatusHistoryAsync(
        ResourceContentVersionEntity contentVersionEntity,
        ResourceContentStatus status,
        int changedByUserId,
        CancellationToken ct)
    {
        var resourceContentVersionStatusHistory = new ResourceContentVersionStatusHistoryEntity
        {
            ResourceContentVersion = contentVersionEntity,
            Status = status,
            ChangedByUserId = changedByUserId,
            Created = DateTime.UtcNow,
        };

        await _dbContext.ResourceContentVersionStatusHistory.AddAsync(resourceContentVersionStatusHistory, ct);
    }

    public async Task AddAssignedUserHistoryAsync(
        ResourceContentVersionEntity contentVersionEntity,
        int? assignedUserId,
        int changedByUserId,
        CancellationToken ct)
    {
        var resourceContentVersionAssignedUserHistory = new ResourceContentVersionAssignedUserHistoryEntity
        {
            ResourceContentVersion = contentVersionEntity,
            AssignedUserId = assignedUserId,
            ChangedByUserId = changedByUserId,
            Created = DateTime.UtcNow,
        };

        await _dbContext.ResourceContentVersionAssignedUserHistory.AddAsync(resourceContentVersionAssignedUserHistory, ct);
    }

    public async Task AddSnapshotHistoryAsync(
        ResourceContentVersionEntity contentVersionEntity,
        int? oldUserId,
        ResourceContentStatus oldStatus,
        CancellationToken ct)
    {
        var isNew = contentVersionEntity.Id == 0;
        ResourceContentVersionSnapshotEntity? snapshotEntity = null;

        if (!isNew)
        {
            snapshotEntity = await _dbContext.ResourceContentVersionSnapshots
                .AsTracking()
                .Where(rcvn => rcvn.ResourceContentVersionId == contentVersionEntity.Id)
                .OrderByDescending(x => x.Created)
                .FirstOrDefaultAsync(ct);
        }

        if (snapshotEntity?.Content != contentVersionEntity.Content)
        {
            await AddSnapshotHistoryAsync(
                contentVersionEntity,
                contentVersionEntity.Content,
                contentVersionEntity.DisplayName,
                contentVersionEntity.WordCount,
                oldUserId,
                oldStatus,
                ct);
        }
    }

    public async Task AddSnapshotHistoryAsync(
        ResourceContentVersionEntity contentVersionEntity,
        string content,
        string displayName,
        int? wordCount,
        int? oldUserId,
        ResourceContentStatus oldStatus,
        CancellationToken ct)
    {
        var resourceContentVersionSnapshot = new ResourceContentVersionSnapshotEntity
        {
            ResourceContentVersion = contentVersionEntity,
            Content = content,
            DisplayName = displayName,
            WordCount = wordCount,
            UserId = oldUserId,
            Status = oldStatus,
            Created = DateTime.UtcNow,
        };

        await _dbContext.ResourceContentVersionSnapshots.AddAsync(resourceContentVersionSnapshot, ct);
    }
}