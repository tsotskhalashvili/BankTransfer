using BankTransfer.Application.DTOs;
using BankTransfer.Application.Interfaces.Repositories;
using BankTransfer.Application.Interfaces.Services;
using BankTransfer.Domain.Exceptions;

namespace BankTransfer.Application.Services;

public class TransferService : ITransferService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransferService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionDto> TransferAsync(TransferRequestDto request)
    {
        if (request.FromAccountId == request.ToAccountId)
            throw new InvalidTransferException("Cannot transfer to the same account");

        var fromAccount = await _unitOfWork.Accounts.GetByIdAsync(request.FromAccountId)
            ?? throw new AccountNotFoundException(request.FromAccountId);

        var toAccount = await _unitOfWork.Accounts.GetByIdAsync(request.ToAccountId)
            ?? throw new AccountNotFoundException(request.ToAccountId);

        fromAccount.Withdraw(request.Amount);
        toAccount.Deposit(request.Amount);

        _unitOfWork.Accounts.Update(fromAccount);
        _unitOfWork.Accounts.Update(toAccount);

        var transaction = new Domain.Entities.Transaction(
            request.FromAccountId,
            request.ToAccountId,
            request.Amount);

        await _unitOfWork.Transactions.AddAsync(transaction);
        await _unitOfWork.SaveChangesAsync();

        return new TransactionDto
        {
            Id = transaction.Id,
            FromAccountId = transaction.FromAccountId,
            ToAccountId = transaction.ToAccountId,
            Amount = transaction.Amount,
            OccurredAtUtc = transaction.OccurredAtUtc
        };
    }
}