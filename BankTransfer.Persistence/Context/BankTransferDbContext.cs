using BankTransfer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankTransfer.Persistence.Context;

public class BankTransferDbContext : DbContext
{
    public BankTransferDbContext(DbContextOptions<BankTransferDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankTransferDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}