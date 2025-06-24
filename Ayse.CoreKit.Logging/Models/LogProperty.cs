namespace Ayse.CoreKit.Logging.Models;

public class LogProperty
{
    public string Name { get; set; }
    public object Value { get; set; }
    public string Type { get; set; }

    public LogProperty()
    {
        Name = string.Empty;
        Value = string.Empty;
        Type = string.Empty;
    }

    public LogProperty(string name, object value, string type)
    {
        Name = name;
        Value = value;
        Type = type;
    }

}