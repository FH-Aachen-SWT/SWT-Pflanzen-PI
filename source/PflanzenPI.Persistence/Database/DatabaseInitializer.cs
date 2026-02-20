using System.Data;
using Dapper;
using PflanzenPI.Persistence.Schema;

namespace PflanzenPI.Persistence.Database;

public static class DatabaseInitializer
{
    private static readonly SchemaVersionSchema schemaVersionSchema = new();
    
    private static readonly ISchema[] _tables =
    [
        new TamagotchiSchema(),
    ];
    
    /// <summary>
    /// Initializes the Database
    /// </summary>
    /// <param name="toStartVersion">The version number to start with</param>
    public static async Task InitializeAsync(int toStartVersion)
    {
        await using var connection = await DatabaseConnectionFactory.UnsafeCreate();
        await using var transaction = await connection.BeginTransactionAsync();
        Console.WriteLine("Starting DB initialization");
        Console.WriteLine($"Opened DB: {connection.DataSource}");
        try
        {
            int oldVersion = await GetOrCreateSchemaVersion(connection, transaction);
            if (oldVersion != toStartVersion)
            {
                await UpdateSchemaVersion(connection, transaction, toStartVersion);
            }
            
            if (!await IsSchemaInitalized(connection, transaction))
            {
                if (toStartVersion < oldVersion)
                {
                    await Downgrade(connection, transaction, oldVersion, toStartVersion); //Downgrade to the old lower Version
                } 
                else
                {
                    await Migrate(connection, transaction, oldVersion, toStartVersion); //Migrate to the new higher Version 
                    //Initialize
                    foreach (var table in _tables)
                    {
                        await table.OnInitialize(connection, transaction, toStartVersion);
                    }
                
                }
                await UpdateSchemaInitialized(connection, transaction, toStartVersion);
            }
        
            await transaction.CommitAsync();
            Console.WriteLine("DB initialized successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred in the setup of the database. Rolling back changes. {0}", e);
            await transaction.RollbackAsync();
            throw;
        }
    }

    private static async Task Downgrade(IDbConnection connection, IDbTransaction transaction, int fromVersion, int toVersion)
    {
        for (int currentVersion = fromVersion; currentVersion >= toVersion; currentVersion--)
        {
            //Create
            foreach (var table in _tables)
            {
                await table.OnDowngrade(connection, transaction, currentVersion);
            }
        }
    }
    
    private static async Task Migrate(IDbConnection connection, IDbTransaction transaction, int fromVersion, int toVersion)
    {
        if (fromVersion == 0)
        {
            //Create
            foreach (var table in _tables)
            {
                await table.OnMigrate(connection, transaction, 0);
            }

            return;
        }
        
        for (int currentVersion = fromVersion + 1; currentVersion <= toVersion; currentVersion++)
        {
            //Create
            foreach (var table in _tables)
            {
                await table.OnMigrate(connection, transaction, currentVersion);
            }
        }
    }


    /// <summary>
    /// Set the schemaVersion to initialized so the next run does not call OnCreateTable or OnInitialize
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="transaction"></param>
    /// <param name="schemaVersion"></param>
    private static async Task UpdateSchemaInitialized(IDbConnection connection, IDbTransaction transaction, int schemaVersion)
    {
        await connection.ExecuteScalarAsync("""
                                            UPDATE SchemaVersion 
                                            SET isInitialized = 1 
                                            """, new {VersionNr = schemaVersion}, transaction: transaction);
    }

    private static async Task<bool> IsSchemaInitalized(IDbConnection connection, IDbTransaction transaction)
    {
        return await connection.ExecuteScalarAsync<bool>("""
                                                         SELECT isInitialized
                                                         FROM SchemaVersion
                                                         """ ,transaction);
    }
    
    private static async Task<int> UpdateSchemaVersion(IDbConnection connection, IDbTransaction transaction, int newVersion)
    {
        await connection.ExecuteAsync("""
                                      UPDATE SchemaVersion
                                      SET versionNr = @NewVersion,
                                      isInitialized = 0
                                      
                                      """, new {NewVersion = newVersion}, transaction: transaction);
        Console.WriteLine($"Updated schema version table to {newVersion}");
        return newVersion;
    }

    /// <summary>
    /// Gets the schema Version. If schema Table does not exist, create a new and return 0
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    private static async Task<int> GetOrCreateSchemaVersion(IDbConnection connection, IDbTransaction transaction)
    {
        int schemaVersion = 0;
        if (!await SchemaVersionTableExists(connection))
        {
            await schemaVersionSchema.OnMigrate(connection, transaction, schemaVersion);
            await schemaVersionSchema.OnInitialize(connection, transaction, schemaVersion);
            Console.WriteLine("Created schema version table");
        }
        else
        {
            var version = await connection.ExecuteScalarAsync<int?>("""
                                                                        SELECT versionNr
                                                                        FROM SchemaVersion;
                                                                    """, transaction: transaction);

            schemaVersion = version ?? 0;
        }
        return schemaVersion;
    }
    
    /// <summary>
    /// Check if SchmemaVersion exists.
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    private static async Task<bool> SchemaVersionTableExists(IDbConnection connection)
    {
        var tableExists = await connection.ExecuteScalarAsync<int>("""
                        SELECT COUNT(1)
                        FROM sqlite_master
                        WHERE type='table' AND name=@TableName;
        """,
            new { TableName = "SchemaVersion" });

       return tableExists > 0;
    }
}