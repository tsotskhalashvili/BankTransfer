using BankTransfer.Domain.Entities;

namespace BankTransfer.Application.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<List<Transaction>> GetAllAsync();
}