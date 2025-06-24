using Ayse.CoreKit.Logging.Base;
using Ayse.CoreKit.Logging.Loggers;
using Ayse.CoreKit.Logging.Models.Enums;
using Ayse.CoreKit.Logging.SeriLog.Config;
using Microsoft.Extensions.Configuration;

namespace Ayse.CoreKit.Logging.Factories;

public static class LoggerFactory
{
    public static LogWriterBase CreateLogger(LoggerType type, IConfiguration configuration)
    {
        return type switch
        {
            LoggerType.MsSql =>
                new MsSqlLogger(configuration.GetSection("SeriLogOptions:MsSql").Get<MsSqlOptions>()),
            _ => throw new NotSupportedException($"Logger type '{type}' is not supported.")
        };
    }
}