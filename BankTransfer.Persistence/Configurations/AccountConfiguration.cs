using BankTransfer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankTransfer.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> entity)
    {
        entity.HasKey(a => a.Id);

        entity.Property(a => a.OwnerName)
            .IsRequired()
            .HasMaxLength(200);

        entity.Property(a => a.Balance)
            .HasColumnType("decimal(18,2)");

        entity.Property(a => a.RowVersion)
            .IsRowVersion();
    }
}