using System.Data;
using Dapper;

namespace PflanzenPI.Persistence.Schema;

public class StreakSchema : ISchema
{
    public async Task OnMigrateAsync(IDbConnection connection, IDbTransaction transaction, int schemaVersion)
    {
        switch (schemaVersion)
        {
            case 2:
                await connection.ExecuteAsync("""
                                              CREATE TABLE IF NOT EXISTS Streak(
                                                 date TEXT NOT NULL DEFAULT (CURRENT_DATE),
                                                 startGoal INTEGER NOT NULL,
                                                 endGoal INTEGER NOT NULL,
                                                 owner TEXT NOT NULL REFERENCES Tamagotchi(name) ON DELETE CASCADE,  
                                                 PRIMARY KEY(date, owner)
                                              )
                                              """, transaction: transaction);
                await connection.ExecuteAsync("""
                                               CREATE INDEX IF NOT EXISTS idx_streak_owner ON Streak(owner);
                                              """
                                              );
                break;
        }
    }

    public async Task OnDowngradeAsync(IDbConnection connection, IDbTransaction transaction, int toVersion)
    {
        switch (toVersion)
        {
            case 1:
                await connection.ExecuteAsync("""
                                                DROP TABLE IF EXISTS Streak
                                              """, transaction: transaction);
                break;
                
        }
    }

    public async Task OnInitializeAsync(IDbConnection connection, IDbTransaction transaction, int schemaVersion)
    {
        switch (schemaVersion)
        {
            case 2:
                await connection.ExecuteAsync("""
                                              INSERT INTO Streak (startGoal, endGoal, owner)
                                              VALUES (
                                                  0,
                                                  0,
                                                  (SELECT name FROM Tamagotchi WHERE isSelected = 1 LIMIT 1)
                                              );
                                              
                                              
                                              """, transaction: transaction);
                break;
        }
    }
}