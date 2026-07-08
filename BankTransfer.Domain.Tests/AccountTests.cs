using BankTransfer.Domain.Entities;
using BankTransfer.Domain.Exceptions;
using Xunit;

namespace BankTransfer.Domain.Tests;

public class AccountTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesAccount()
    {
        var account = new Account("Ana Kapanadze", 1000m);

        Assert.Equal("Ana Kapanadze", account.OwnerName);
        Assert.Equal(1000m, account.Balance);
        Assert.NotEqual(Guid.Empty, account.Id);
    }

    [Fact]
    public void Constructor_WithNegativeInitialBalance_ThrowsInvalidTransferException()
    {
        Assert.Throws<InvalidTransferException>(() => new Account("Ana", -100m));
    }

    [Fact]
    public void Constructor_WithEmptyOwnerName_ThrowsInvalidTransferException()
    {
        Assert.Throws<InvalidTransferException>(() => new Account("", 100m));
    }

    [Fact]
    public void Withdraw_WithSufficientBalance_DecreasesBalance()
    {
        var account = new Account("Ana", 1000m);

        account.Withdraw(300m);

        Assert.Equal(700m, account.Balance);
    }

    [Fact]
    public void Withdraw_WithInsufficientBalance_ThrowsInsufficientFundsException()
    {
        var account = new Account("Ana", 100m);

        Assert.Throws<InsufficientFundsException>(() => account.Withdraw(500m));
    }

    [Fact]
    public void Withdraw_WithNegativeAmount_ThrowsInvalidTransferException()
    {
        var account = new Account("Ana", 1000m);

        Assert.Throws<InvalidTransferException>(() => account.Withdraw(-50m));
    }

    [Fact]
    public void Withdraw_WithZeroAmount_ThrowsInvalidTransferException()
    {
        var account = new Account("Ana", 1000m);

        Assert.Throws<InvalidTransferException>(() => account.Withdraw(0m));
    }

    [Fact]
    public void Withdraw_ExactBalance_ResultsInZeroBalance()
    {
        var account = new Account("Ana", 500m);

        account.Withdraw(500m);

        Assert.Equal(0m, account.Balance);
    }

    [Fact]
    public void Deposit_WithPositiveAmount_IncreasesBalance()
    {
        var account = new Account("Ana", 1000m);

        account.Deposit(250m);

        Assert.Equal(1250m, account.Balance);
    }

    [Fact]
    public void Deposit_WithNegativeAmount_ThrowsInvalidTransferException()
    {
        var account = new Account("Ana", 1000m);

        Assert.Throws<InvalidTransferException>(() => account.Deposit(-10m));
    }

    [Fact]
    public void Deposit_WithZeroAmount_ThrowsInvalidTransferException()
    {
        var account = new Account("Ana", 1000m);

        Assert.Throws<InvalidTransferException>(() => account.Deposit(0m));
    }
}