using System.Data;
using Dapper;

namespace PflanzenPI.Persistence.Schema;

public class BrightnessSchema : ISchema
{
    public async Task OnMigrateAsync(IDbConnection connection, IDbTransaction transaction, int schemaVersion)
    {
        switch (schemaVersion)
        {
            case 3:
                await connection.ExecuteAsync("""
                                              CREATE TABLE IF NOT EXISTS Brightness(
                                                  owner TEXT NOT NULL REFERENCES Tamagotchi(name) ON DELETE CASCADE PRIMARY KEY,
                                                  hour1 REAL NOT NULL,
                                                  hour2 REAL NOT NULL,
                                                  hour3 REAL NOT NULL,
                                                  hour4 REAL NOT NULL,
                                                  hour5 REAL NOT NULL,
                                                  hour6 REAL NOT NULL,
                                                  hour7 REAL NOT NULL,
                                                  hour8 REAL NOT NULL,
                                                  hour9 REAL NOT NULL,
                                                  hour10 REAL NOT NULL,
                                                  hour11 REAL NOT NULL,
                                                  hour12 REAL NOT NULL,
                                                  hour13 REAL NOT NULL,
                                                  hour14 REAL NOT NULL,
                                                  hour15 REAL NOT NULL,
                                                  hour16 REAL NOT NULL,
                                                  hour17 REAL NOT NULL,
                                                  hour18 REAL NOT NULL,
                                                  hour19 REAL NOT NULL,
                                                  hour20 REAL NOT NULL,
                                                  hour21 REAL NOT NULL,
                                                  hour22 REAL NOT NULL,
                                                  hour23 REAL NOT NULL,
                                                  hour24 REAL NOT NULL
                                                  )
                                              """, transaction: transaction);
                await connection.ExecuteAsync("""
                                              CREATE INDEX IF NOT EXISTS idx_brightness_owner ON Brightness(owner);
                                              """);
                break;
        }
    }

    public Task OnDowngradeAsync(IDbConnection connection, IDbTransaction transaction, int toVersion)
    {
        return Task.CompletedTask;
    }

    public async Task OnInitializeAsync(IDbConnection connection, IDbTransaction transaction, int schemaVersion)
    {
        switch (schemaVersion)
        {
            case 3:
                await connection.ExecuteAsync("""
                                              INSERT OR REPLACE INTO Brightness(owner, hour1, hour2, hour3, hour4, hour5, hour6, hour7, hour8, hour9, hour10, hour11, hour12, hour13, hour14, hour15, hour16, hour17, hour18, hour19, hour20, hour21, hour22, hour23, hour24)
                                              VALUES (
                                                  (SELECT name FROM Tamagotchi WHERE isSelected = 1 LIMIT 1),
                                                  3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000
                                              );
                                              """, transaction: transaction);
                break;
        }
    }
}