﻿using CleanArchitectureSample.Application.Common.Interfaces;
using CleanArchitectureSample.Domain.Common;
using CleanArchitectureSample.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArchitectureSample.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor(IUser user, TimeProvider dateTime) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<IAuditable>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(AuditColumn.Created).CurrentValue = dateTime.GetUtcNow();
                entry.Property(AuditColumn.CreatedBy).CurrentValue = user.Id;
            }

            if (entry.State == EntityState.Added ||
                entry.State == EntityState.Modified ||
                HasChangedOwnedEntities(entry))
            {
                entry.Property(AuditColumn.LastModified).CurrentValue = dateTime.GetUtcNow();
                entry.Property(AuditColumn.LastModifiedBy).CurrentValue = user.Id;
            }
        }
    }

    private static bool HasChangedOwnedEntities(EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
