using Microsoft.EntityFrameworkCore;
using PicPay.Domain.Entities;
using PicPay.Domain.Interfaces;
using PicPay.Infrastructure.Data;

namespace PicPay.Infrastructure.Repositories;

public class TransactionRepository(PicPayDbContext context) : ITransactionRepository
{
    private readonly PicPayDbContext _context = context;

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await _context.Transactions.FindAsync(id);
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.ToListAsync();
    }

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task UpdateAsync(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Transaction transaction)
    {
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByUserAsync(Guid userId)
    {
        return await _context.Transactions
            .AsNoTracking()
            .Where(t => t.ToUserId == userId)
            .ToListAsync();
    }
}