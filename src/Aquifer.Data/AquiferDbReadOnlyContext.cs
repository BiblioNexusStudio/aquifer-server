using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aquifer.Data;

public sealed class AquiferDbReadOnlyContext(DbContextOptions<AquiferDbReadOnlyContext> _options)
    : AquiferDbContext(_options, true)
{
    private const string ErrorMessage =
        $"{nameof(AquiferDbReadOnlyContext)} is read-only. Use {nameof(AquiferDbContext)} for write operations.";

#pragma warning disable IDE0022 // Use block body for method (justification: all we're doing is throwing an exception)

    public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override EntityEntry Add(object entity)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = new())
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = new())
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override void AddRange(params object[] entities)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override void AddRange(IEnumerable<object> entities)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override Task AddRangeAsync(params object[] entities)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = new())
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override EntityEntry Attach(object entity)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override void AttachRange(params object[] entities)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override void AttachRange(IEnumerable<object> entities)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override EntityEntry Remove(object entity)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override void RemoveRange(params object[] entities)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override void RemoveRange(IEnumerable<object> entities)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override int SaveChanges()
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override EntityEntry Update(object entity)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override void UpdateRange(params object[] entities)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

    public override void UpdateRange(IEnumerable<object> entities)
    {
        throw new InvalidOperationException(ErrorMessage);
    }

#pragma warning restore IDE0022 // Use block body for method
}