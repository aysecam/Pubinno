using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pubinno.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pubinno.Persistence.Configurations
{
    public class PourEventConfiguration : IEntityTypeConfiguration<PourEvent>
    {
        public void Configure(EntityTypeBuilder<PourEvent> builder)
        {
            builder.ToTable("pour_events");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.EventId)
                .HasColumnName("event_id")
                .IsRequired();

            builder.Property(x => x.DeviceId)
                .HasColumnName("device_id")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LocationId)
                .HasColumnName("location_id")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ProductId)
                .HasColumnName("product_id")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.StartedAt)
                .HasColumnName("started_at")
                .IsRequired();

            builder.Property(x => x.EndedAt)
                .HasColumnName("ended_at")
                .IsRequired();

            builder.Property(x => x.VolumeMl)
                .HasColumnName("volume_ml")
                .IsRequired();

            // BaseEntity alanları
            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasColumnName("created_by")
                .HasMaxLength(100);

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at");

            builder.Property(x => x.UpdatedBy)
                .HasColumnName("updated_by")
                .HasMaxLength(100);

            builder.Property(x => x.DeletedAt)
                .HasColumnName("deleted_at");

            builder.Property(x => x.DeletedBy)
                .HasColumnName("deleted_by")
                .HasMaxLength(100);

            // EventId unique index — idempotency için
            builder.HasIndex(x => x.EventId)
                .IsUnique()
                .HasDatabaseName("ix_pour_events_event_id");

            // Summary query'leri için composite index
            builder.HasIndex(x => new { x.DeviceId, x.StartedAt })
                .HasDatabaseName("ix_pour_events_device_id_started_at");
        }
    }
}
