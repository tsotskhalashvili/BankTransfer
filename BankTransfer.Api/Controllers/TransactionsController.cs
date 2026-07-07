using BankTransfer.Application.DTOs;
using BankTransfer.Application.Interfaces.Repositories;
using BankTransfer.Application.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace BankTransfer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<List<TransactionDto>>> GetAll()
    {
        var transactions = await _unitOfWork.Transactions.GetAllAsync();
        var dtos = transactions.Select(t => t.ToDto()).ToList();

        return Ok(dtos);
    }
}