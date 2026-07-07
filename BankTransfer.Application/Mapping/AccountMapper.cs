using BankTransfer.Application.DTOs;
using BankTransfer.Domain.Entities;

namespace BankTransfer.Application.Mapping;

public static class AccountMapper
{
    public static AccountDto ToDto(this Account account)
    {
        return new AccountDto
        {
            Id = account.Id,
            OwnerName = account.OwnerName,
            Balance = account.Balance
        };
    }
}