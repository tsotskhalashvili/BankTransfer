using BankTransfer.Application.DTOs;
using BankTransfer.Application.Interfaces.Repositories;
using BankTransfer.Application.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace BankTransfer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<List<AccountDto>>> GetAll()
    {
        var accounts = await _unitOfWork.Accounts.GetAllAsync();
        var dtos = accounts.Select(a => a.ToDto()).ToList();

        return Ok(dtos);
    }
}