using Dapper;
using System.Data;

namespace PflanzenPI.Persistence.Schema;

public class TamagotchiSchema : ISchema
{
    public async Task OnMigrateAsync(IDbConnection connection, IDbTransaction transaction, int schemaVersion)
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
                                                CREATE UNIQUE INDEX IF NOT EXISTS UX_OnlyOneSelected
                                                ON Tamagotchi(isSelected)
                                                WHERE isSelected = 1;
                                              """, transaction: transaction);
                break;
            case 1:
                await connection.ExecuteAsync("""
                                              ALTER TABLE Tamagotchi
                                              ADD COLUMN personalityType INTEGER NOT NULL DEFAULT 0
                                              """);
                break;

        }

    }

    public async Task OnDowngradeAsync(IDbConnection connection, IDbTransaction transaction, int toVersion)
    {
        switch (toVersion)
        {
            case 0:
                await connection.ExecuteAsync("""
                                              ALTER TABLE Tamagotchi
                                              DROP COLUMN personalityType;
                                              """, transaction: transaction);
                break;
        }
    }

    public async Task OnInitializeAsync(IDbConnection connection, IDbTransaction transaction, int schemaVersion)
    {
        switch (schemaVersion)
        {
            case 0:
                await connection.ExecuteAsync("""
                                        INSERT INTO Tamagotchi(name, plantType, brightnessType, isSelected)
                                        VALUES ("Bob B.", 1, 1, 1);
                                        """, transaction: transaction);
                break;
            case 1:
                await connection.ExecuteAsync("""
                                              UPDATE Tamagotchi
                                              SET personalityType = 1
                                              """, transaction: transaction);
                break;
        }
    }
}