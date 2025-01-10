using Microsoft.EntityFrameworkCore;
using PicPay.Domain.Entities;
using PicPay.Domain.Interfaces;
using PicPay.Infrastructure.Data;

namespace PicPay.Infrastructure.Repositories;

public class UserRepository(PicPayDbContext context) : IUserRepository
{
    private readonly PicPayDbContext _context = context;

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByDocumentAsync(string document)
    {
        return await _context.Users.FirstOrDefaultAsync(u =>  u.Document == document);
    }

    public async Task<bool> DocumentExistsAsync(string document)
    {
        return await _context.Users.AnyAsync(u =>   u.Document == document);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}