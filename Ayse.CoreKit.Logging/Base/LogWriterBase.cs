using Serilog;

namespace Ayse.CoreKit.Logging.Base;

public abstract class LogWriterBase
{
    protected ILogger Logger;

    protected LogWriterBase(ILogger logger)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public void Verbose(string message) => Logger.Verbose(message);
    public void Verbose(string message, params object[] args) => Logger.Verbose(string.Format(message, args));
    
    public void Fatal(string message) => Logger.Fatal(message);
    public void Info(string message) => Logger.Information(message);
    public void Warn(string message) => Logger.Warning(message);
    public void Debug(string message) => Logger.Debug(message);
    
    public void Error(string message) => Logger.Error(message);
    public void Error(string message, params object[] args) => Logger.Error(string.Format(message, args));
    public void Error(Exception ex,string message) => Logger.Error(ex, message);
    
}