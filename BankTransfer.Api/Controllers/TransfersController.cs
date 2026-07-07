using BankTransfer.Application.DTOs;
using BankTransfer.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankTransfer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransfersController : ControllerBase
{
    private readonly ITransferService _transferService;

    public TransfersController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpPost]
    public async Task<ActionResult<TransactionDto>> Transfer([FromBody] TransferRequestDto request)
    {
        var result = await _transferService.TransferAsync(request);
        return Ok(result);
    }
}