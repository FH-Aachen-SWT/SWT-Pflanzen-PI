using PflanzenPi.Sensor;

namespace PflanzenPi.Plants.PredictionModel;

public class PredictionService : IPredictionService
{
    private readonly TimeSpan _pollingRate;

    private int _samples = 0;
    private float _addedDifference = 0.0f;
    private float _lastMoisture = -1.0f;
    
    public PredictionService(TimeSpan pollingRate)
    {
        _pollingRate = pollingRate;
    }

    public void AddSample(Moisture moisture)
    {
        if (_samples++ != 0 || _lastMoisture != -1.0f)
        {
            _addedDifference += moisture - _lastMoisture;
        }
        _lastMoisture = moisture;
    }
    
    public TimeSpan? PredictTimeUntilThreshold(Moisture threshold)
    {
        float time = (threshold - _lastMoisture) / AverageDifference() * (float)_pollingRate.TotalSeconds;
        return time > 0? TimeSpan.FromSeconds(time): null;
    }
    
    private float AverageDifference()
    {
        return _addedDifference / _samples;
    }
}