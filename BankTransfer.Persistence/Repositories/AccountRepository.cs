using BankTransfer.Application.Interfaces.Repositories;
using BankTransfer.Domain.Entities;
using BankTransfer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BankTransfer.Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BankTransferDbContext _context;

    public AccountRepository(BankTransferDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByIdAsync(Guid id)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Account>> GetAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public void Update(Account account)
    {
        _context.Accounts.Update(account);
    }
}