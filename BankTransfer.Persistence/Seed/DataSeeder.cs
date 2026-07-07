using BankTransfer.Domain.Entities;
using BankTransfer.Persistence.Context;

namespace BankTransfer.Persistence.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(BankTransferDbContext context)
    {
        if (context.Accounts.Any())
            return;

        var accounts = new List<Account>
        {
            new Account("Ana Kapanadze", 1000m),
            new Account("Giorgi Beridze", 500m),
            new Account("Nino Tabatadze", 250m)
        };

        context.Accounts.AddRange(accounts);
        await context.SaveChangesAsync();
    }
}