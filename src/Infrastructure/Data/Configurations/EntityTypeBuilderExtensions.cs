using CleanArchitectureSample.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitectureSample.Infrastructure.Data.Configurations;

public static class EntityTypeBuilderExtensions
{
    public static void AddAuditColumns<T>(this EntityTypeBuilder<T> builder) where T : class, IAuditable
    {
        builder
            .Property<DateTimeOffset>(AuditColumn.LastModified);

        builder
            .Property<DateTimeOffset>(AuditColumn.Created);

        builder
            .Property<string>(AuditColumn.CreatedBy)
            .HasMaxLength(100);

        builder
            .Property<string>(AuditColumn.LastModifiedBy)
            .HasMaxLength(100);
    }
}
