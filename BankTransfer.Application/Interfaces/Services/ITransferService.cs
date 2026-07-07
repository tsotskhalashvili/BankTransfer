using BankTransfer.Application.DTOs;

namespace BankTransfer.Application.Interfaces.Services;

public interface ITransferService
{
    Task<TransactionDto> TransferAsync(TransferRequestDto request);
}