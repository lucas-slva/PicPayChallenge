using PicPay.Application.DTOs;
using PicPay.Domain.Entities;

namespace PicPay.Application.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(UserDto userDto);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(Guid userId);
}