namespace Ayse.CoreKit.Logging.SeriLog.Config;

public class MsSqlOptions
{
    public string ConnectionString { get; set; }
    public string TableName { get; set; }
    public bool AutoCreateSqlTable { get; set; }

    public MsSqlOptions()
    {
        ConnectionString = String.Empty;
        TableName = String.Empty;
    }

    public MsSqlOptions(string connectionString, string tableName, bool autoCreateSqlTable )
    {
            ConnectionString = connectionString;
            TableName = tableName;
            AutoCreateSqlTable = autoCreateSqlTable;
    }
}