namespace PicPay.Application.DTOs;

public record TransactionDto(
    Guid FromUserId,
    Guid ToUserId,
    decimal Amount
    );