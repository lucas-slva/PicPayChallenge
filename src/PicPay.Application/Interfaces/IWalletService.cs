using PicPay.Application.DTOs;
using PicPay.Domain.Entities;

namespace PicPay.Application.Interfaces;

public interface IWalletService
{
    Task<Wallet> CreateWalletAsync(WalletDto walletDto);
    Task<Wallet?> GetWalletByUserIdAsync(Guid userId);
    Task UpdateWalletBalanceAsync(Guid userId, decimal amount);
}