using PflanzenPi.Sensor.Sensors;

namespace PflanzenPi.Plants.PredictionModel;

public class PredictionService : IPredictionService
{
    public event WateringPredictionChangedEvent? OnPredictionUpdated;
    private readonly TimeSpan _pollingRate;
    
    private int _samples = 0;
    private int _acceptedSamples = 0;
    private float _addedDifference = 0.0f;
    private float _lastMoisture = -1.0f;

    private readonly int _predictEveryXSamples;
    private readonly int _samplesUntilFirstPrediction;
    
    public PredictionService(TimeSpan pollingRate, int samplesUntilFirstPrediction, int predictEveryXSamples)
    {
        _pollingRate = pollingRate;
        _samplesUntilFirstPrediction = samplesUntilFirstPrediction;
        _predictEveryXSamples = predictEveryXSamples;   
    }

    /// <summary>
    /// Adds a sample to the prediction model. This must happen periodically or der prediction will be wrong.
    /// </summary>
    public void AddSample(Moisture moisture)
    {
        if (_samples++ != 0)
        {
            float difference = moisture - _lastMoisture;
            if (Math.Abs(difference) < 0.5f * _pollingRate.TotalSeconds )// [value] * pollingRate.TotalSeconds to make sure weird values reset the prediction. The larger the pollingRate, the better the model
            { 
                _addedDifference += difference;
                _acceptedSamples++;
            }
            else // resets model if the moisture changes by more than 5% in 10 seconds
            {
                Reset();
            }
        }
        _lastMoisture = moisture;
    }



    /// <summary>
    /// Predicts the time until the moisture level reaches a certain threshold
    /// </summary>
    /// <param name="threshold">Moisturelevel you want to calculate the time for</param>
    public TimeSpan? PredictTimeUntilThreshold(Moisture threshold)
    {
        float average = AverageDifference();
        if (_samples >= _samplesUntilFirstPrediction &&  _samples % _predictEveryXSamples == 0 &&  MathF.Abs(average) > 1e-6f) // returns first value after _samplesUntilFirstPrediction is reached
        {
            float time = (threshold - _lastMoisture) / average * (float)_pollingRate.TotalSeconds;
            TimeSpan? newTime = time > 0 ? TimeSpan.FromSeconds(time) : null;
            if (newTime.HasValue)
            {
                OnPredictionUpdated?.Invoke(newTime.Value);
            }
            return newTime;
        }
        return null;
    }
    
    /// <summary>
    /// Calculates the average difference between all samples
    /// </summary>
    /// <returns></returns>
    private float AverageDifference()
    {
        if (_acceptedSamples == 0) 
            return 0.0f;
        return _addedDifference / _acceptedSamples;
    }

    /// <summary>
    /// Resets the prediction service to its initial state
    /// </summary>
    public void Reset()
    {
        _samples = 0;
        _acceptedSamples = 0;
        _addedDifference = 0.0f;
        _lastMoisture = -1.0f;
    }
}