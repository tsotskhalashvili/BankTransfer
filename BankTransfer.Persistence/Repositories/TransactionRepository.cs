using BankTransfer.Application.Interfaces.Repositories;
using BankTransfer.Domain.Entities;
using BankTransfer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BankTransfer.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly BankTransferDbContext _context;

    public TransactionRepository(BankTransferDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
    }

    public async Task<List<Transaction>> GetAllAsync()
    {
        return await _context.Transactions
            .OrderByDescending(t => t.OccurredAtUtc)
            .ToListAsync();
    }
}