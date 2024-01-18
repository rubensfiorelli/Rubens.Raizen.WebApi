using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Rubens.Raizen.WebApi.Entities;

namespace Test.Rubens.Raizen.WebApi.Database
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(x => x.Id);
                       
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder.Property(x => x.LastName)
              .IsRequired()
              .HasColumnType("NVARCHAR")
              .HasMaxLength(80);

            builder.Property(x => x.Email)
             .IsRequired()
             .HasColumnType("VARCHAR")
             .HasMaxLength(80);

            builder.Property(x => x.PasswordHash)
             .IsRequired()
             .HasColumnType("VARCHAR")
             .HasMaxLength(256);

            builder.Property(x => x.ZipCode)
             .IsRequired()
             .HasColumnType("VARCHAR")
             .HasMaxLength(9);

            builder
               .HasIndex(x => x.Email, "IX_Customer_Email")
               .IsUnique();

            builder
             .Property(c => c.CreatedAt)
             .HasDefaultValueSql("GETDATE()")
             .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore
             .Metadata.PropertySaveBehavior.Ignore);

            builder
              .Property(c => c.UpdatedAt)
              .HasDefaultValueSql("GETDATE()")
              .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore
              .Metadata.PropertySaveBehavior.Ignore);

        }
    }
}
