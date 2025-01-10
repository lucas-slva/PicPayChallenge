namespace PicPay.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }

    public Guid FromUserId { get; set; } // sender user

    public Guid ToUserId { get; set; } // receiver user

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsSuccessful { get; set; }
    
    public virtual required User FromUser { get; set; }
    public virtual required User ToUser { get; set; }
}