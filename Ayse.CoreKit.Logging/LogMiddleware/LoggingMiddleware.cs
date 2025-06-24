using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ayse.CoreKit.Logging.LogMiddleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceLogService _logService;

    public LoggingMiddleware(RequestDelegate next, IServiceLogService logService)
    {
        _next = next;
        _logService = logService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var transactionId = Guid.NewGuid();
        var createdDate = DateTime.UtcNow;
        var userIdentifier = context.User?.Identity?.IsAuthenticated == true
            ? context.User.Identity.Name
            : "anonymous";

        // --- REQUEST LOGGING ---
        context.Request.EnableBuffering();
        string requestBody = await ReadStreamAsync(context.Request.Body);
        context.Request.Body.Position = 0;

        var requestLog = new ServiceLog
        {
            Path = context.Request.Path,
            Method = context.Request.Method,
            QueryString = context.Request.QueryString.ToString(),
            Body = requestBody,
            IsRequest = true,
            TransactionId = transactionId,
            CreatedDate = createdDate,
            UserIdentifier = userIdentifier
        };

        await _logService.Add(requestLog);

        // --- RESPONSE LOGGING ---
        var originalBodyStream = context.Response.Body;
        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        await _next(context); // devam et

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        var responseLog = new ServiceLog
        {
            Path = context.Request.Path,
            Method = context.Request.Method,
            StatusCode = context.Response.StatusCode,
            Body = responseBody,
            IsRequest = false,
            TransactionId = transactionId,
            CreatedDate = DateTime.UtcNow,
            UserIdentifier = userIdentifier
        };

        await _logService.Add(responseLog);

        await responseBodyStream.CopyToAsync(originalBodyStream); // geri yaz
    }

    private async Task<string> ReadStreamAsync(Stream stream)
    {
        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
        return await reader.ReadToEndAsync();
    }
}
