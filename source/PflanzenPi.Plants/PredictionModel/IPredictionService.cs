using PflanzenPi.Sensor;

namespace PflanzenPi.Plants.PredictionModel;

/// <summary>
/// Provides a minimal prediction API for estimating when a moisture threshold is reached.
/// </summary>
public interface IPredictionService
{
    /// <summary>
    /// Adds a moisture sample
    /// </summary>
    /// <param name="moisture">Moisture in percent (0-100).</param>
    public void AddSample(Moisture moisture);

    /// <summary>
    /// Predicts the remaining time until the specified moisture threshold is reached.
    /// Returns null when there are too few samples or no decreasing trend exists.
    /// </summary>
    /// <param name="threshold">Threshold in percent (0-100).</param>
    public TimeSpan? PredictTimeUntilThreshold(Moisture threshold);
}