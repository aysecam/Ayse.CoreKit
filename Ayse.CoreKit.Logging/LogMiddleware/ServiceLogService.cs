using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Ayse.CoreKit.Logging.LogMiddleware;
public class ServiceLogService : IServiceLogService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ServiceLogService> _logger;
    private readonly string _connectionString;
    private readonly string _tableName;

    public ServiceLogService(IConfiguration configuration, ILogger<ServiceLogService> logger)
    {
        _configuration = configuration;
        _logger = logger;

        _connectionString = _configuration.GetSection("MsSqlConfig:ConnectionString").Value!;
        _tableName = _configuration.GetSection("MsSqlConfig:TableName").Value!;
        var needAutoCreateTable = _configuration.GetSection("MsSqlConfig:NeedAutoCreateTable").Get<bool>();

        if (needAutoCreateTable)
        {
            EnsureTableExists();
        }
    }

    public async System.Threading.Tasks.Task Add(ServiceLog log)
    {
        try
        {
            var query = $@"s
                INSERT INTO {_tableName}
                (Path, Method, Body, QueryString, StatusCode, IsRequest, UserIdentifier, TransactionId, CreatedDate)
                VALUES
                (@Path, @Method, @Body, @QueryString, @StatusCode, @IsRequest, @UserIdentifier, @TransactionId, @CreatedDate)";

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(query, log);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while inserting the log to the database.");
            throw;
        }
    }

    private void EnsureTableExists()
    {
        try
        {
            var query = $@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{_tableName}')
                BEGIN
                    CREATE TABLE {_tableName} (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Path NVARCHAR(2000),
                        Method NVARCHAR(20),
                        Body NVARCHAR(MAX),
                        QueryString NVARCHAR(MAX),
                        StatusCode INT NULL,
                        IsRequest BIT,
                        UserIdentifier NVARCHAR(250),
                        TransactionId UNIQUEIDENTIFIER,
                        CreatedDate DATETIME
                    )
                END";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            connection.Execute(query);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while ensuring the log table exists.");
            throw;
        }
    }
} 