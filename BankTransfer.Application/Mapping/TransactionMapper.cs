using BankTransfer.Application.DTOs;
using BankTransfer.Domain.Entities;

namespace BankTransfer.Application.Mapping;

public static class TransactionMapper
{
    public static TransactionDto ToDto(this Transaction transaction)
    {
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