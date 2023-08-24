using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users")
                .HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.FirstName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.LastName)
                 .HasMaxLength(150)
                 .IsRequired();

            builder.Property(x => x.Password)
                 .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(0);

            builder.Property(x => x.LastModifiedAtUtc)
                 .IsRequired(false);
        }
    }
}
