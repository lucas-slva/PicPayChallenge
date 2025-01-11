using Microsoft.AspNetCore.Mvc;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;

namespace PicPay.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class WalletController(IWalletService walletService) : ControllerBase
{
    private readonly IWalletService _walletService = walletService;

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<WalletDto>> GetWalletByUserAsync(Guid userId)
    {
        var wallet = await _walletService.GetWalletByUserIdAsync(userId);
        
        return wallet is null ? NotFound() : Ok(wallet);
    }
    
    [HttpPost]
    public async Task<ActionResult<WalletDto>> CreateWalletAsync(WalletDto walletDto)
    {
        var wallet = await _walletService.CreateWalletAsync(walletDto with { Balance = 0 });
        
        return Ok(wallet);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateWalletBalanceAsync(WalletDto walletDto)
    {
        await _walletService.UpdateWalletBalanceAsync(walletDto.UserId, walletDto.Balance);
        
        return Ok("Funds added successfully");
    }
}