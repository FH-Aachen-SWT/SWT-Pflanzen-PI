namespace PflanzenPI.Persistence.Business;

public interface IBrightnessService
{
    /// <summary>
    /// Set brightness for specified hour
    /// </summary>
    /// <param name="hour"></param>
    /// <param name="lux"></param>
    /// <returns></returns>
    Task<Result<BrightnessError>> SetBrightnessForHourAsync(int hour, double lux);

    /// <summary>
    /// Get DLI from the last 24 hours
    /// </summary>
    /// <returns></returns>
    Task<double> GetDLIAsync();

    /// <summary>
    /// Add new tamagotchi to brightness table
    /// </summary>
    /// <returns></returns>
    Task<Result<BrightnessError>> AddTamagotchiToBrightness();
}