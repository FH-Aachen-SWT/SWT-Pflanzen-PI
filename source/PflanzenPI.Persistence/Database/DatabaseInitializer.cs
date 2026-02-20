using Dapper;
using PflanzenPI.Persistence.Database.TypeHandler;
using PflanzenPI.Persistence.Schema;
using System.Data;

namespace PflanzenPI.Persistence.Database;

public static class DatabaseInitializer
{
    private static readonly SchemaVersionSchema schemaVersionSchema = new();

    private static readonly ISchema[] _tables =
    [
        new TamagotchiSchema(),
        new StreakSchema()
    ];

    /// <summary>
    /// Initializes the Database
    /// </summary>
    /// <param name="toStartVersion">The version number to start with</param>
    public static async Task InitializeAsync(int toStartVersion)
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        await using var connection = await DatabaseConnectionFactory.UnsafeCreate();
        await using var transaction = await connection.BeginTransactionAsync();

        Console.WriteLine("Starting DB initialization");
        Console.WriteLine($"Opened DB: {connection.DataSource}");

        try
        {
            int? oldVersion = await GetOrCreateSchemaVersionAsync(connection, transaction);
            bool freshCreate = oldVersion is null;

            Console.WriteLine($"Old Version: {oldVersion}, Target Version: {toStartVersion}");

            // ---- MIGRATION / DOWNGRADE ----
            if (oldVersion < toStartVersion || freshCreate)
            {
                await MigrateAsync(connection, transaction, oldVersion, toStartVersion);
                await UpdateSchemaVersionAsync(connection, transaction, toStartVersion, true);
            }
            else if (oldVersion > toStartVersion)
            {
                await DowngradeAsync(connection, transaction, oldVersion.Value, toStartVersion);
                await UpdateSchemaVersionAsync(connection, transaction, toStartVersion, false);
            }

            // ---- INITIALIZATION ----
            if (!await IsSchemaInitalizedAsync(connection, transaction))
            {
                await InitializeAsync(connection, transaction, oldVersion, toStartVersion);
                await UpdateSchemaInitializedAsync(connection, transaction, toStartVersion);
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

    private static async Task DowngradeAsync(IDbConnection connection, IDbTransaction transaction, int fromVersion, int toVersion)
    {
        for (int currentVersion = fromVersion - 1; currentVersion >= toVersion; currentVersion--)
        {
            //Create
            foreach (var table in _tables)
            {
                await table.OnDowngradeAsync(connection, transaction, currentVersion);
            }
        }
    }

    private static async Task MigrateAsync(IDbConnection connection, IDbTransaction transaction, int? fromVersion, int toVersion)
    {
        if (fromVersion is null)
        {
            //Create
            foreach (var table in _tables)
            {
                await table.OnMigrateAsync(connection, transaction, 0);
            }

            if (toVersion == 0)
            {
                return;
            }
        }

        for (int currentVersion = fromVersion is not null ? fromVersion.Value + 1 : 1; currentVersion <= toVersion; currentVersion++)
        {
            //Create
            foreach (var table in _tables)
            {
                await table.OnMigrateAsync(connection, transaction, currentVersion);
            }
        }
    }

    private static async Task InitializeAsync(IDbConnection connection, IDbTransaction transaction, int? fromVersion, int toVersion)
    {
        if (fromVersion is null)
        {
            foreach (var table in _tables)
            {
                await table.OnInitializeAsync(connection, transaction, 0);
            }

            if (toVersion == 0)
            {
                return;
            }
        }


        for (int currentVersion = fromVersion is not null ? fromVersion.Value + 1 : 1; currentVersion <= toVersion; currentVersion++)
        {
            foreach (var table in _tables)
            {
                await table.OnInitializeAsync(connection, transaction, currentVersion);
            }
        }
    }


    /// <summary>
    /// Set the schemaVersion to initialized so the next run does not call OnMigrateAsync or OnInitializeAsync
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="transaction"></param>
    /// <param name="schemaVersion"></param>
    private static async Task UpdateSchemaInitializedAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        int schemaVersion)
    {
        await connection.ExecuteAsync("""
        UPDATE SchemaVersion 
        SET isInitialized = 1
        WHERE versionNr = @VersionNr;
    """,
        new { VersionNr = schemaVersion },
        transaction: transaction);
    }

    private static async Task<bool> IsSchemaInitalizedAsync(
        IDbConnection connection,
        IDbTransaction transaction)
    {
        return await connection.ExecuteScalarAsync<bool>("""
                                                             SELECT isInitialized
                                                             FROM SchemaVersion
                                                             LIMIT 1;
                                                         """, transaction: transaction);
    }

    private static async Task UpdateSchemaVersionAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        int newVersion,
        bool withUninitializedSchema)
    {
        await connection.ExecuteAsync("""
                                          UPDATE SchemaVersion
                                          SET versionNr = @NewVersion,
                                              isInitialized = @Initialized
                                          WHERE 1 = 1;
                                      """,
            new { NewVersion = newVersion, Initialized = withUninitializedSchema ? 0 : 1 },
            transaction: transaction);

        Console.WriteLine($"Updated schema version table to {newVersion}");
    }

    /// <summary>
    /// Gets the schema Version. If schema Table does not exist, create a new and return 0
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    private static async Task<int?> GetOrCreateSchemaVersionAsync(IDbConnection connection, IDbTransaction transaction)
    {
        if (!await SchemaVersionTableExistsAsync(connection))
        {
            await schemaVersionSchema.OnMigrateAsync(connection, transaction, 0);
            await schemaVersionSchema.OnInitializeAsync(connection, transaction, 0);
            Console.WriteLine("Created schema version table");
            return null;
        }
        var version = await connection.QuerySingleOrDefaultAsync<int?>("""
                                                                    SELECT versionNr
                                                                    FROM SchemaVersion
                                                                    ORDER BY versionNr DESC
                                                                LIMIT 1;
                                                                """, transaction: transaction);

        return version;

    }

    /// <summary>
    /// Check if SchmemaVersion exists.
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    private static async Task<bool> SchemaVersionTableExistsAsync(IDbConnection connection)
    {
        var tableExists = await connection.QuerySingleAsync<int>("""
                        SELECT COUNT(1)
                        FROM sqlite_master
                        WHERE type='table' AND name=@TableName;
        """,
            new { TableName = "SchemaVersion" });

        return tableExists > 0;
    }
}