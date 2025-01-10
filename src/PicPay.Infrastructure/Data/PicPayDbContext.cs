using Microsoft.EntityFrameworkCore;
using PicPay.Domain.Entities;

namespace PicPay.Infrastructure.Data;

public class PicPayDbContext(DbContextOptions<PicPayDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PicPayDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}