using BankTransfer.Domain.Entities;

namespace BankTransfer.Application.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id);
    Task<List<Account>> GetAllAsync();
    void Update(Account account);
}