namespace Ayse.CoreKit.Logging.Models;

public class LogDetail
{
    public string FullName { get; set; }
    public string MethodName { get; set; }
    public string User { get; set; }
    public List<LogProperty> Parameters { get; set; }

    public LogDetail()
    {
        FullName = string.Empty;
        MethodName = string.Empty;
        User = string.Empty;
        Parameters = new List<LogProperty>();
    }

    public LogDetail(string fullName, string methodName, string user, List<LogProperty> parameters)
    {
        FullName = fullName;
        MethodName = methodName;
        User = user;
        Parameters = parameters;
    }
}


