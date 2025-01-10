namespace PicPay.Application.DTOs;

public record UserDto(
    string FullName,
    string Document,
    string Email,
    bool IsMerchant
    );