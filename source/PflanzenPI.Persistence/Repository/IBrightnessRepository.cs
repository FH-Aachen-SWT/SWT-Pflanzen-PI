using PflanzenPI.Persistence.Schema;

namespace PflanzenPI.Persistence.Repository;

public interface IBrightnessRepository
{
    Task<BrightnessEntity?> GetBrightnessByHourAsync(int hour);

    Task UpdateBrightnessByHourAsync(int hour, double lux);

    Task AddTamagotchiToBrightnessAsync();
}