namespace PflanzenPI.Persistence.Schema.Model;

/// <summary>
/// Datenbankmodell für eine Streak 
/// </summary>
/// <param name="Date"></param>
/// <param name="StartGoal"></param>
/// <param name="EndGoal"></param>
public record StreakEntity(DateOnly Date, long StartGoal, long EndGoal, string Owner)
{
    public StreakEntity(string Date, long StartGoal, long EndGoal, string Owner) : this(DateOnly.Parse(Date), StartGoal,
        EndGoal, Owner)
    {
        
    }
}