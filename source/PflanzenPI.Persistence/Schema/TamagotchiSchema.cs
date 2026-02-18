using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using PflanzenPI.Persistence.Database;

namespace PflanzenPI.Persistence.Schema;

public class TamagotchiSchema : ISchema
{
    public async Task OnMigrate(IDbConnection connection, IDbTransaction transaction, int schemaVersion)
    {
        switch (schemaVersion)
        {
            case 0:
                await connection.ExecuteAsync(
                    """
                        CREATE TABLE IF NOT EXISTS Tamagotchi(
                            name TEXT PRIMARY KEY CHECK (LENGTH(name) <= 15),
                            brightnessType INTEGER NOT NULL,
                            plantType INTEGER NOT NULL,
                            isSelected INTEGER NOT NULL CHECK (isSelected BETWEEN 0 AND 1)
                        );
                    """,
                transaction: transaction);
                await connection.ExecuteAsync("""
                                                CREATE UNIQUE INDEX UX_OnlyOneSelected
                                                ON Tamagotchi(isSelected)
                                                WHERE isSelected = 1;
                                              """, transaction: transaction);
                break;
            
        }

    }

    public Task OnDowngrade(IDbConnection connection, IDbTransaction transaction, int toVersion)
    {
        return Task.CompletedTask;
    }

    public async Task OnInitialize(IDbConnection connection, IDbTransaction transaction, int schemaVersion)
    {
        switch (schemaVersion)
        {
            case 0:
                await connection.ExecuteAsync("""
                                        INSERT INTO Tamagotchi(name, plantType, brightnessType, isSelected) VALUES ("Bob B.", 1, 1, 1);
                                        """, transaction: transaction);
                break;       
        }
    }
}