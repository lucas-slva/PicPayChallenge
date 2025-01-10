using FluentValidation;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;
using PicPay.Domain.Entities;
using PicPay.Domain.Exceptions;
using PicPay.Domain.Interfaces;

namespace PicPay.Application.Services;

public class WalletService(
    IWalletRepository walletRepository,
    IUserRepository userRepository,
    IValidator<WalletDto> walletValidator) 
    : IWalletService
{
    private readonly IWalletRepository _walletRepository = walletRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator<WalletDto> _walletValidator = walletValidator;
    
    public async Task<Wallet> CreateWalletAsync(WalletDto walletDto)
    {
        var validationResult = await _walletValidator.ValidateAsync(walletDto);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var user = await _userRepository.GetByIdAsync(walletDto.UserId) ?? throw new UserNotFoundException(walletDto.UserId);
        
        var wallet = new Wallet
        {
            User = user,
            UserId = walletDto.UserId,
            Balance = walletDto.Balance
        };
        
        await _walletRepository.CreateAsync(wallet);
        return wallet;
    }

    public async Task<Wallet?> GetWalletByUserIdAsync(Guid userId)
    {
        return await _walletRepository.GetByUserIdAsync(userId);
    }

    public async Task UpdateWalletBalanceAsync(Guid userId, decimal amount)
    {
        var wallet = await _walletRepository.GetByUserIdAsync(userId) ?? throw new WalletNotFoundException(userId);

        wallet.Balance += amount;
        await _walletRepository.UpdateAsync(wallet);
    }
}