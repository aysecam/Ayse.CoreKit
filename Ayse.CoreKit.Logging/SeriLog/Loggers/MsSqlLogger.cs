using Ayse.CoreKit.Logging.Base;
using Ayse.CoreKit.Logging.SeriLog.Config;
using Ayse.CoreKit.Logging.SeriLog.Templates;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Ayse.CoreKit.Logging.Loggers;

public class MsSqlLogger : LogWriterBase
{
    public MsSqlLogger(MsSqlOptions options)
        : base(CreateLogger(options))
    {
    }

    private static ILogger CreateLogger(MsSqlOptions options)
    {
        if (options == null)
            throw new Exception(SeriLogTemplates.NullOptionsMessage);

        var sinkOptions = new MSSqlServerSinkOptions
        {
            TableName = options.TableName,
            AutoCreateSqlTable = options.AutoCreateSqlTable
        };

        var columnOptions = new ColumnOptions();

        return new LoggerConfiguration()
            .WriteTo.MSSqlServer(options.ConnectionString, sinkOptions, columnOptions: columnOptions)
            .CreateLogger();
    }
}
