namespace BankTransfer.Application.DTOs;

public class AccountDto
{
    public Guid Id { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}