namespace PflanzenPi.Plants.PredictionModel;

public class PredictionService : IPredictionService
{
    private TimeSpan _pollingRate;

    private int _samples = 0;
    private float _addedDifference = 0;
    private float _lastMoisture = -1;
    
    public PredictionService(TimeSpan pollingRate)
    {
        _pollingRate = pollingRate;
    }

    public void AddSample(float moisture)
    {
        if (_samples++ != 0)
        {
            _addedDifference += moisture - _lastMoisture;
        }
        _lastMoisture = moisture;
    }
    
    public TimeSpan? PredictTimeUntilThreshold(float threshold)
    {
        float time = (threshold - _lastMoisture) / AverageDifference() * (float)_pollingRate.TotalSeconds;
        return time > 0? TimeSpan.FromSeconds(time): null;
    }
    
    private float AverageDifference()
    {
        return _addedDifference / _samples;
    }
}