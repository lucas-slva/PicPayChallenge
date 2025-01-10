using FluentValidation;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;
using PicPay.Domain.Entities;
using PicPay.Domain.Interfaces;

namespace PicPay.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IValidator<UserDto> userValidator) 
    : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator<UserDto> _userValidator = userValidator;
    
    public async Task<User> CreateUserAsync(UserDto userDto)
    {
        var validationResult = await _userValidator.ValidateAsync(userDto);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var user = new User
        {
            FullName = userDto.FullName,
            Document = userDto.Document,
            Email = userDto.Email,
            IsMerchant = userDto.IsMerchant
        };
        
        if (await _userRepository.DocumentExistsAsync(user.Document))
            throw new Exception("Document already exists."); 

        if (await _userRepository.EmailExistsAsync(user.Email))
            throw new Exception("Email already exists.");

        return await _userRepository.CreateAsync(user);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _userRepository.GetByIdAsync(userId);
    }
}