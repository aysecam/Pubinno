using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pubinno.Domain.Entities;

namespace Pubinno.Persistence.Configurations
{
    public class ExceptionLogConfiguration : IEntityTypeConfiguration<ExceptionLog>
    {
        public void Configure(EntityTypeBuilder<ExceptionLog> builder)
        {
            builder.ToTable("exception_logs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Path)
                .HasColumnName("path")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Method)
                .HasColumnName("method")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Message)
                .HasColumnName("message")
                .IsRequired();

            builder.Property(x => x.StackTrace)
                .HasColumnName("stack_trace");

            builder.Property(x => x.StatusCode)
                .HasColumnName("status_code")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.DeletedAt)
                .HasColumnName("deleted_at");
        }
    }
}
