using PflanzenPI.Persistence.Business.Errors;
using PflanzenPI.Persistence.Repository;

namespace PflanzenPI.Persistence.Business;

public class StreakService : IStreakService
{
    private readonly IStreakRepository _repository;
    
    public StreakService(IStreakRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<StreakError>> SetGoalReachedAsync(string tamagotchiName)
    {
        var goalReachedResult = await IsGoalReachedAsync(tamagotchiName);

        if (goalReachedResult.IsFailure)
            return Result<StreakError>.Failure(goalReachedResult.Error!);

        var goalReached = goalReachedResult.Value;

        if (goalReached is not null)
        {
            return Result<StreakError>.Failure(
                new StreakError.GoalAlreadyReached(goalReached.Value));
        }

        var streakEntity = await _repository.GetStreakForYesterdayAsync(tamagotchiName);

        long newEndGoal = streakEntity?.EndGoal + 1 ?? 1;

        await _repository.UpdateTodaysStreakAsync(tamagotchiName, newEndGoal);

        return Result<StreakError>.Success();
    }


    public async Task<ValueResult<long?, StreakError>> IsGoalReachedAsync(string tamagotchiName)
    {
        var streakToday = await _repository.GetStreakForTodayAsync(tamagotchiName);
        if (streakToday is null)
        {
            return ValueResult<long?, StreakError>.Failure(new StreakError.TodayDoesNotExist());
        }

        return streakToday.StartGoal != streakToday.EndGoal ?  ValueResult<long?, StreakError>.Success(streakToday.EndGoal) : ValueResult<long?, StreakError>.Success(null);
    }

    public async Task<Result<StreakError>> CreateNewDayAsync(string tamagotchiName)
    {
        if (await _repository.GetStreakForTodayAsync(tamagotchiName) is not null)
        {
            return Result<StreakError>.Failure(new StreakError.TodayAlreadyExists());
        }
        long startGoal = 0;
        var yesterDayStreak = await _repository.GetStreakForYesterdayAsync(tamagotchiName);
        if (yesterDayStreak is not null)
        {
            startGoal = yesterDayStreak.StartGoal != yesterDayStreak.EndGoal ? yesterDayStreak.EndGoal : 0;
        }
        await _repository.CreateStreakForTodayAsync(tamagotchiName, startGoal);
        return Result<StreakError>.Success();
    }

    public async Task<long?> GetTodaysStreakAsync(string tamagotchiName)
    {
        return (await _repository.GetStreakForTodayAsync(tamagotchiName))?.EndGoal ?? 0;
    }
}