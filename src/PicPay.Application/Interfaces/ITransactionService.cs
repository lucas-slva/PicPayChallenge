using PicPay.Application.DTOs;
using PicPay.Domain.Entities;

namespace PicPay.Application.Interfaces;

public interface ITransactionService
{
    Task<Transaction> ProcessTransactionAsync(TransactionDto transactionDto);
}