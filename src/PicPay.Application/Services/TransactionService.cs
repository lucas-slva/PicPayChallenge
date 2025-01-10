using FluentValidation;
using PicPay.Application.DTOs;
using PicPay.Application.Interfaces;
using PicPay.Domain.Entities;
using PicPay.Domain.Exceptions;
using PicPay.Domain.Interfaces;

namespace PicPay.Application.Services;

public class TransactionService(
    ITransactionRepository transactionRepository,
    IWalletRepository walletRepository,
    IUserRepository userRepository,
    IValidator<TransactionDto> transactionValidator)
    : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IWalletRepository _walletRepository = walletRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IValidator<TransactionDto> _transactionValidator = transactionValidator;


    public async Task<Transaction> ProcessTransactionAsync(TransactionDto transactionDto)
    {
        var validationResult = await _transactionValidator.ValidateAsync(transactionDto);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var sender = await _userRepository.GetByIdAsync(transactionDto.FromUserId) ?? throw new UserNotFoundException(transactionDto.FromUserId);
        var receiver = await _userRepository.GetByIdAsync(transactionDto.ToUserId) ?? throw new UserNotFoundException(transactionDto.ToUserId);
        
        if (sender.IsMerchant)
            throw new UnauthorizedTransactionException();
        
        var senderWallet = await _walletRepository.GetByUserIdAsync(sender.Id) ?? throw new WalletNotFoundException(sender.Id);
        
        if (senderWallet.Balance < transactionDto.Amount) throw new InsufficientBalanceException(senderWallet.Balance, transactionDto.Amount);
        
        senderWallet.Balance -= transactionDto.Amount;
        
        var transaction = new Transaction
        {
            FromUser = sender,
            ToUser = receiver,
            FromUserId = transactionDto.FromUserId,
            ToUserId = transactionDto.ToUserId,
            Amount = transactionDto.Amount,
            IsSuccessful = true,
        };

        await _transactionRepository.CreateAsync(transaction);
        await _walletRepository.UpdateAsync(senderWallet);
        return transaction;
    }
}