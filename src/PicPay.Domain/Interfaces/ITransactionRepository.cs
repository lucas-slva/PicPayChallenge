using PicPay.Domain.Entities;

namespace PicPay.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<Transaction> CreateAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
    Task DeleteAsync(Transaction transaction);

    Task<IEnumerable<Transaction>> GetByUserAsync(Guid userId);
}