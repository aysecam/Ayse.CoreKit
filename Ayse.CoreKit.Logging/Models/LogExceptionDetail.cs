namespace Ayse.CoreKit.Logging.Models;

public class LogExceptionDetail : LogDetail
{
    public string ExceptionMessage { get; set; }
    
        public LogExceptionDetail()
        {
            ExceptionMessage = string.Empty;
        }

        public LogExceptionDetail(string fullName, string methodName, string user, List<LogProperty> parameters, string exceptionMessage)
            : base(fullName, methodName, user, parameters)
        {
            ExceptionMessage = exceptionMessage;
        }
    }
    