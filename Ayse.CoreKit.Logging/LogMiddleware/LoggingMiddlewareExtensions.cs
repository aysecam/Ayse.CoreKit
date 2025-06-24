using Microsoft.AspNetCore.Builder; 
namespace Ayse.CoreKit.Logging.LogMiddleware;

public static class LoggingMiddlewareExtensions
{
    public static void UseRequestResponseLoggin(this IApplicationBuilder app) => app.UseMiddleware<LoggingMiddleware>();
}