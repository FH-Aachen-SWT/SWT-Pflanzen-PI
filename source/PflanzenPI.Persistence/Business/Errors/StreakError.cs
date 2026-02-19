namespace PflanzenPI.Persistence.Business.Errors;

public abstract record StreakError : Error
{
    public abstract string Message { get; }
    private StreakError() { }

    public sealed record GoalAlreadyReached(long CurrGoal) : StreakError
    {
        public override string Message => $"Goal has already been reached today. Current: {CurrGoal}";
    }

    public sealed record TodayDoesNotExist : StreakError
    {
        public override string Message => "Todays streak does not exist";
    }
    
    public sealed record TodayAlreadyExists() : StreakError
    {
        public override string Message => "Todays streak already exists";
    }
    

}
