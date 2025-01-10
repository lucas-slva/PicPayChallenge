using PicPay.Domain.Entities;

namespace PicPay.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
        
    Task<User?> GetByDocumentAsync(string document);
    Task<bool> DocumentExistsAsync(string document);
    Task<bool> EmailExistsAsync(string email);
}