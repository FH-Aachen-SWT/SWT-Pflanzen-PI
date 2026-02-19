namespace PflanzenPI.Persistence.Business;

public interface IStreakBatch
{
    /// <summary>
    /// Schedules the next creation for midnight and every day to come until app closes for the tamagotchi
    /// </summary>
    /// <returns></returns>
    Task StartScheduling(string tamagotchiName);
    
    /// <summary>
    /// Stops the scheduling
    /// </summary>
    /// <returns></returns>
    Task StopScheduling();
}