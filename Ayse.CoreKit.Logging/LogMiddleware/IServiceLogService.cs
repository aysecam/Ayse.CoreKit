namespace Ayse.CoreKit.Logging.LogMiddleware;

public interface IServiceLogService
{ 
    public Task Add(ServiceLog log);   
}