namespace BankTransfer.Domain.Exceptions;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(Guid accountId, decimal requested, decimal available)
        : base($"Account {accountId} has insufficient funds. Requested: {requested}, Available: {available}")
    {
    }
}