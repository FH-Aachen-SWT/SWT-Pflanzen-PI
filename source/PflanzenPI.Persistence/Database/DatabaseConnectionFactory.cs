using Microsoft.Data.Sqlite;

namespace PflanzenPI.Persistence.Database;

public static class DatabaseConnectionFactory
{
    private static readonly string CONNECTION_STRING = "DataSource=pflanze.db";
    
    /// <summary>
    /// Flag to show that the DB is initialized and every next request does not have to initialize
    /// </summary>
    private static bool isInitalized;
    public static int Version { get; set; }
    
    /// <summary>
    /// Creates a new Connection to the SQLite DB
    /// </summary>
    /// <returns></returns>
    public static async Task<SqliteConnection> Create()
    {
        if (!isInitalized)
        {
            await DatabaseInitializer.InitializeAsync(Version);
        }
        isInitalized = true;
        var connection = new SqliteConnection(CONNECTION_STRING);
        await connection.OpenAsync();
        return connection;
    }

    /// <summary>
    /// Creates a new connection to the SQLite DB without checking if the DB is initialized
    /// </summary>
    /// <returns></returns>
    public static async Task<SqliteConnection> UnsafeCreate()
    {
        var connection = new SqliteConnection(CONNECTION_STRING);
        await connection.OpenAsync();
        return connection;
    }
}