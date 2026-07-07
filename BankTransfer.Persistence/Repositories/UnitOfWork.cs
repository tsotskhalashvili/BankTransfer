using BankTransfer.Application.Interfaces.Repositories;
using BankTransfer.Persistence.Context;

namespace BankTransfer.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BankTransferDbContext _context;

    public IAccountRepository Accounts { get; }
    public ITransactionRepository Transactions { get; }

    public UnitOfWork(BankTransferDbContext context)
    {
        _context = context;
        Accounts = new AccountRepository(context);
        Transactions = new TransactionRepository(context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}