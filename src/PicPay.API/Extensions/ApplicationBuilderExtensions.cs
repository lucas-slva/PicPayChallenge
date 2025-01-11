using PicPay.API.Middlewares;

namespace PicPay.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureApplication(this WebApplication app)
    {
        // Middleware
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        // Auth
        app.UseAuthentication();
        app.UseAuthorization();

        // Controllers
        app.MapControllers();
        
        // Use https
        app.UseHttpsRedirection();
    }
}