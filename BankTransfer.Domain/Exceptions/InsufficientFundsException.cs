namespace BankTransfer.Domain.Exceptions;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(Guid accountId, string ownerName, decimal requested, decimal available)
        : base($"Account '{ownerName}' has insufficient funds. Requested: {requested}, Available: {available}")
    {
    }
}