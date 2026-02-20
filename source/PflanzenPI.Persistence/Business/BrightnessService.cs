using PflanzenPI.Persistence.Repository;

namespace PflanzenPI.Persistence.Business;

public class BrightnessService : IBrightnessService
{
    private readonly IBrightnessRepository _repo;
    private const double CalibrationFactor = 0.0185;
    private const int HoursPerDay = 24;
    private const int SecondsPerHour = 3600;
    private const int OneMillionByPacoRabanne = 1000000;

    public BrightnessService(IBrightnessRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<Result<BrightnessError>> SetBrightnessForHourAsync(int hour, double lux)
    {
        var currentBrightness = await _repo.GetBrightnessByHourAsync(hour);
        if (currentBrightness is null)
        {
            return Result<BrightnessError>.Failure(new BrightnessError.OwnerDoesNotExist("default"));
        }

        await _repo.UpdateBrightnessByHourAsync(hour, lux);

        return Result<BrightnessError>.Success();
    }

    public async Task<double> GetDLIAsync()
    {
        var currentBrightness = await _repo.GetBrightnessByHourAsync(1);
        if (currentBrightness is null)
        {
            return 0;
        }
        double? avg = 0;
        for (int i = 1; i <= 24; i++)
        {
            avg += _repo.GetBrightnessByHourAsync(i).Result?.hourLux;
        }
        
        // Lux => PPFD
        if (avg is null)
        {
            return 0;
        }

        var avgPPFD = ((double)avg/HoursPerDay) * CalibrationFactor; // avg/24 ist  der durchschnitt des stündlichen durchsnitts-lux

        return (avgPPFD * SecondsPerHour * HoursPerDay) / OneMillionByPacoRabanne;
    }

    public async Task<Result<BrightnessError>> AddTamagotchiToBrightness()
    {
        var brightness = await _repo.GetBrightnessByHourAsync(1);
        if (brightness is not null)
        {
            return Result<BrightnessError>.Failure(new BrightnessError.OwnerAlreadyExists("default"));
        }

        await _repo.AddTamagotchiToBrightnessAsync();

        return Result<BrightnessError>.Success();
    }
}