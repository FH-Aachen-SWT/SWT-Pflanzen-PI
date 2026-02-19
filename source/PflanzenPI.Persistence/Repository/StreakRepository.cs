using Dapper;
using PflanzenPI.Persistence.Database;
using PflanzenPI.Persistence.Schema.Model;

namespace PflanzenPI.Persistence.Repository;

public class StreakRepository : IStreakRepository
{
    public async Task<StreakEntity?> GetStreakAsync(DateOnly date, string tamagotchiName)
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        var streakEntity = await connection.QuerySingleOrDefaultAsync<StreakEntity>("""
                                        SELECT date as Date, startGoal as StartGoal, endGoal as EndGoal, owner as Owner
                                        FROM Streak
                                        WHERE date = @Date AND owner = @TamagotchiName
                                      """, new {Date = date.ToString("yyyy-MM-dd"),  TamagotchiName = tamagotchiName});
        return streakEntity;
    }

    public async Task UpdateStreakAsync(DateOnly date, string tamagotchiName, long newEndGoal)
    {
       await using var connection = await DatabaseConnectionFactory.Create();
       await connection.ExecuteScalarAsync("""
                                            UPDATE Streak 
                                            SET endGoal = @EndGoal
                                            WHERE date = @Date AND owner = @TamagotchiName
                                           """, new {EndGoal = newEndGoal,  Date = date, TamagotchiName = tamagotchiName}); 
       
    }

    public async Task CreateStreakForTodayAsync(string tamagotchiName, long startGoal)
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        await connection.ExecuteAsync("""
                                        INSERT INTO Streak(owner, startGoal, endGoal) 
                                        VALUES (@TamagotchiName, @StartGoal, @StartGoal)
                                      """, new {TamagotchiName = tamagotchiName,  StartGoal = startGoal});
    }
}