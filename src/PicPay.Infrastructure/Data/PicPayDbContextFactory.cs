using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PicPay.Infrastructure.Data;

public class PicPayDbContextFactory   : IDesignTimeDbContextFactory<PicPayDbContext>
{
    public PicPayDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PicPayDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=PicPayDB;User ID=sa;Password=MyStrong@Pwd123!;TrustServerCertificate=True");

        return new PicPayDbContext(optionsBuilder.Options);
    }
}