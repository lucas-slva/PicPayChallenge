using Microsoft.EntityFrameworkCore;
using PicPay.Domain.Entities;
using PicPay.Domain.Interfaces;
using PicPay.Infrastructure.Data;

namespace PicPay.Infrastructure.Repositories;

public class WalletRepository(PicPayDbContext context) : IWalletRepository
{
    private readonly PicPayDbContext _context = context;
    
    public async Task<Wallet?> GetByIdAsync(Guid id)
    {
        return await _context.Wallets.FindAsync(id);
    }

    public async Task<IEnumerable<Wallet>> GetAllAsync()
    {
        return await _context.Wallets.ToListAsync();
    }

    public async Task<Wallet> CreateAsync(Wallet wallet)
    {
        await _context.Wallets.AddAsync(wallet);
        await  _context.SaveChangesAsync();
        return wallet;
    }

    public async Task UpdateAsync(Wallet wallet)
    {
        _context.Wallets.Update(wallet);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Wallet wallet)
    {
        _context.Wallets.Remove(wallet);
        await _context.SaveChangesAsync();
    }

    public async Task<Wallet?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
    }
}