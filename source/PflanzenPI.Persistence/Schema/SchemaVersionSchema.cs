using System.Data;
using Dapper;


namespace PflanzenPI.Persistence.Schema;

/// <summary>
/// Special table for holding the current Schema version. Only one Row. Table itself has no versioning
/// </summary>
public class SchemaVersionSchema : ISchema
{
    
    public async Task OnMigrate(IDbConnection connection, IDbTransaction transaction,  int schemaVersion) 
    {
        await connection.ExecuteAsync(
            """
                CREATE TABLE IF NOT EXISTS SchemaVersion(
                    versionNr INTEGER PRIMARY KEY AUTOINCREMENT,
                    isInitialized INTEGER NOT NULL CHECK (isInitialized BETWEEN 0 AND 1)
                    )            
            """,
        transaction: transaction);
    }

    public Task OnDowngrade(IDbConnection connection, IDbTransaction transaction, int toVersion)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Only call when the DB has no version yet
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="transaction"></param>
    /// <param name="schemaVersion"></param>
    /// <returns></returns>
    public Task OnInitialize(IDbConnection connection, IDbTransaction transaction, int schemaVersion)
    {
        return connection.ExecuteScalarAsync("""
                                             INSERT INTO SchemaVersion(versionNr, isInitialized) 
                                             VALUES (0, 0); 
                                             """, transaction: transaction);
    }
}