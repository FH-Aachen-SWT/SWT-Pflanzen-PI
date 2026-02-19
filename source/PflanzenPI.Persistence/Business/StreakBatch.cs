using PflanzenPI.Persistence.Business.Errors;
using System.Threading;

namespace PflanzenPI.Persistence.Business;

public class StreakBatch : IStreakBatch
{
    private readonly IStreakService _streakService;
    private Timer? _dailyThreadTimer;
    private string? currentTamagotchiName;

    public StreakBatch(IStreakService streakService)
    {
        _streakService = streakService;
    }

    public async Task StartScheduling(string tamagotchiName)
    {
        currentTamagotchiName =  tamagotchiName;
        var now = DateTime.Now;
        var nextRun = now.Date.AddDays(1); // 0 am
        var initialDelay = nextRun - now + TimeSpan.FromSeconds(2); //2 Sekunden delay, damit wirklich der nächste Tag gennommen wird
        
        // run once immediately
        await Perform(null);

        _dailyThreadTimer = new Timer(state =>
        {
            _ = Perform(null);
        }, null, initialDelay, TimeSpan.FromDays(1));
    }

    private async Task Perform(object? _)
    {
        var streakCreationResult = await _streakService.CreateNewDayAsync(currentTamagotchiName!);
        if (streakCreationResult.IsFailure)
        {
            switch (streakCreationResult.Error)
            {
                case StreakError.TodayAlreadyExists:
                    Console.WriteLine(streakCreationResult.Error.Message);
                    break;
            }
        }
    }

    public async Task StopScheduling()
    {
        if (_dailyThreadTimer != null)
        {
            await _dailyThreadTimer.DisposeAsync();
        }
    }
        
}