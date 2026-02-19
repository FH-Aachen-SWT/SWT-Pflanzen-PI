using PflanzenPI.Persistence.Schema.Model;

namespace PflanzenPI.Persistence.Repository;

/// <summary>
/// Repository für Streaks
/// </summary>
public interface IStreakRepository
{
    /// <summary>
    /// Get the streak for the date and the tamagotchi with name tamagotchiName
    /// </summary>
    /// <param name="date"></param>
    /// <param name="tamagotchiName"></param>
    /// <returns></returns>
    Task<StreakEntity?> GetStreakAsync(DateOnly date, string tamagotchiName);
    
    /// <summary>
    /// Get the streak for today and the tamagotchi with name tamagotchiName
    /// </summary>
    /// <param name="tamagotchiName"></param>
    /// <returns></returns>
    async Task<StreakEntity?> GetStreakForTodayAsync(string tamagotchiName) => await GetStreakAsync(DateOnly.FromDateTime(DateTime.UtcNow), tamagotchiName);
    
    /// <summary>
    /// Get the streak for yesterday and the tamagotchi with name tamagotchiName
    /// </summary>
    /// <param name="tamagotchiName"></param>
    /// <returns></returns>
    async Task<StreakEntity?> GetStreakForYesterdayAsync(string tamagotchiName) => await GetStreakAsync(DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-1), tamagotchiName);

    /// <summary>
    /// Update goal for the date and tamagotchi 
    /// </summary>
    /// <param name="date"></param>
    /// <param name="tamagotchiName"></param>
    /// <param name="newEndGoal"></param>
    /// <returns></returns>
    Task UpdateStreakAsync(DateOnly date, string tamagotchiName, long newEndGoal);

    /// <summary>
    /// Update todays goal for the tamagotchi
    /// </summary>
    /// <param name="tamagotchiName"></param>
    /// <param name="newEndGoal"></param>
    /// <returns></returns>
    async Task UpdateTodaysStreakAsync(string tamagotchiName, long newEndGoal) =>
        await UpdateStreakAsync(DateOnly.FromDateTime(DateTime.UtcNow), tamagotchiName, newEndGoal);

    /// <summary>
    /// Creates a new Streak entry for today and the tamagotchi and the starting goal
    /// </summary>
    /// <param name="date"></param>
    /// <param name="tamagotchiName"></param>
    /// <returns></returns>
    Task CreateStreakForTodayAsync(string tamagotchiName, long startGoal);
    
}