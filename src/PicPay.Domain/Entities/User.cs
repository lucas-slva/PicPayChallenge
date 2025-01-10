namespace PicPay.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public required string FullName { get; set; }

    public required string Document { get; set; }

    public required string Email { get; set; }

    public bool IsMerchant { get; set; }
}