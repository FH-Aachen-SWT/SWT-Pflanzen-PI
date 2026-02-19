using PflanzenPI.Persistence.Business.Errors;
using PflanzenPI.Persistence.Schema.Model;

namespace PflanzenPI.Persistence.Business;

/// <summary>
/// Streak service for performing streak logic
/// </summary>
public interface IStreakService
{
    /// <summary>
    /// Sets the goal of the tamagotchi with the updated goal for today
    /// </summary>
    /// <param name="tamagotchiName"></param>
    /// <returns></returns>
    Task<Result<StreakError>> SetGoalReachedAsync(string tamagotchiName);
    
    /// <summary>
    /// Get if the tamagotchi has reached its goal today
    /// </summary>
    /// <param name="tamagotchiName"></param>
    /// <returns>The value or null</returns>
    Task<ValueResult<long?, StreakError>> IsGoalReachedAsync(string tamagotchiName);
    
    /// <summary>
    /// Creates the new day and takes the goal from yesterday. If goal was not reached, it is reset to 0
    /// </summary>
    /// <param name="tamagotchiName"></param>
    /// <returns></returns>
    Task<Result<StreakError>> CreateNewDayAsync(string tamagotchiName);

    /// <summary>
    /// Get the streak of today
    /// </summary>
    /// <param name="tamagotchiName"></param>
    /// <returns></returns>
    Task<long?> GetTodaysStreakAsync(string tamagotchiName);
}