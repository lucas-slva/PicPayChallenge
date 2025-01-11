using PicPay.Domain.Entities;

namespace PicPay.Domain.Interfaces;

public interface IWalletRepository
{
    Task<Wallet?> GetByIdAsync(Guid id);
    Task<IEnumerable<Wallet>> GetAllAsync();
    Task<Wallet> CreateAsync(Wallet wallet);
    Task UpdateAsync(Wallet wallet);
    Task<Wallet?> GetByUserIdAsync(Guid userId);
    Task UpdateWalletBalanceAsync(Guid userId, decimal amount);
}