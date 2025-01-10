namespace PicPay.Application.DTOs;

public record WalletDto(
    Guid UserId,
    decimal Balance
    );