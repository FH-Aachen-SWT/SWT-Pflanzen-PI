using System.Data;
using Microsoft.Data.Sqlite;

namespace PflanzenPI.Persistence.Schema;

public interface ISchema
{
    /// <summary>
    /// Gets called first when migrating to the schemaversion
    /// Starting from the last version, every onMigrate schema is called
    /// </summary>
    /// <param name="schemaVersion">Starts at 0</param>
    ///<param name="transaction"></param>
    /// <param name="connection"></param>
    /// <returns></returns>
    Task OnMigrate(IDbConnection connection, IDbTransaction transaction, int schemaVersion);
    
    /// <summary>
    /// Gets called first when downgrading to the schemaversion
    /// Starting from the last version, every onDowngrade is called
    /// Example: Current schema version = 1.
    ///         toVersion = 0.
    ///         "case 0:" will get called, which should remove all changes from OnMigrate version 1 aswell as OnInitialize version 1
    /// </summary>
    /// <param name="toVersion">Starts at 0</param>
    ///<param name="transaction"></param>
    /// <param name="connection"></param>
    /// <returns></returns>
    Task OnDowngrade(IDbConnection connection, IDbTransaction transaction, int toVersion);

    /// <summary>
    /// Gets called after the OnMigrate method. Gets called only once if the schmemaVersion is new
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="transaction"></param>
    /// <param name="schemaVersion">Starts at 0</param>
    /// <returns></returns>
    Task OnInitialize(IDbConnection connection, IDbTransaction transaction, int schemaVersion);
}