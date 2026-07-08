using BankTransfer.Domain.Exceptions;

namespace BankTransfer.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }

    public string OwnerName { get; private set; } = string.Empty;
    public decimal Balance { get; private set; }
    public byte[] RowVersion { get; private set; } = Array.Empty<byte>();

    private Account()
    {

    }

    public Account(string ownerName, decimal initialBalance)
    {
        if (string.IsNullOrWhiteSpace(ownerName))
            throw new InvalidTransferException("Owner name cannot be empty");

        if (initialBalance < 0)
            throw new InvalidTransferException("Initial balance cannot be negative");

        Id = Guid.NewGuid();
        OwnerName = ownerName;
        Balance = initialBalance;
    }


    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidTransferException("Withdrawal amount must be positive");

        if (Balance < amount)
            throw new InsufficientFundsException(Id, OwnerName, amount, Balance);

        Balance -= amount;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidTransferException("Deposit amount must be positive");

        Balance += amount;
    }


}
