using Dapper;
using Microsoft.Data.Sqlite;

namespace PflanzenPI.Persistence.Database;

public static class DatabaseConnectionFactory
{
    private static readonly string CONNECTION_STRING = "DataSource=pflanze.db";
    
    /// <summary>
    /// Flag to show that the DB is initialized and every next request does not have to initialize
    /// </summary>
    public static bool IsInitalized { get; private set; }
    public static int Version { get; set; }
    
    /// <summary>
    /// Creates a new Connection to the SQLite DB
    /// </summary>
    /// <returns></returns>
    public static async Task<SqliteConnection> Create()
    {
        if (!IsInitalized)
        {
            await DatabaseInitializer.InitializeAsync(Version);
        }
        IsInitalized = true;
        return await UnsafeCreate();
    }

    /// <summary>
    /// Creates a new connection to the SQLite DB without checking if the DB is initialized
    /// </summary>
    /// <returns></returns>
    public static async Task<SqliteConnection> UnsafeCreate()
    {
        var connection = new SqliteConnection(CONNECTION_STRING);
        await connection.OpenAsync();
        await connection.ExecuteAsync("""
                                        PRAGMA foreign_keys = ON
                                      """); //ENFORCE FOREIGN KEYS
        return connection;
    }
}