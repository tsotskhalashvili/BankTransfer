namespace BankTransfer.Domain.Entities;

public class Transaction
{
    public Guid Id { get; private set; }
    public Guid FromAccountId { get; private set; }
    public Guid ToAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime OccurredAtUtc { get; private set; }

    private Transaction() { }

    public Transaction(Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        Id = Guid.NewGuid();
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
        Amount = amount;
        OccurredAtUtc = DateTime.UtcNow;
    }
}