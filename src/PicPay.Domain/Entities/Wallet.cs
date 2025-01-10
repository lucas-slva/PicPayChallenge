namespace PicPay.Domain.Entities;

public class Wallet
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Balance { get; set; }

    public virtual required User User { get; set; }
}