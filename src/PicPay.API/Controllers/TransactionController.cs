using Microsoft.AspNetCore.Mvc;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;

namespace PicPay.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TransactionController(ITransactionService transactionService) : ControllerBase
{
    private readonly ITransactionService _transactionService = transactionService;

    [HttpPost]
    public async Task<ActionResult<TransactionDto>> CreateTransactionAsync(TransactionDto transactionDto)
    {
        var transaction = await _transactionService.ProcessTransactionAsync(transactionDto);
        
        return Ok(transaction);
    }
}