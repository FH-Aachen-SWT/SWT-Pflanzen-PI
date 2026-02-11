namespace PflanzenPi.Sensor;

/// <summary>
/// Service for the sensors
/// </summary>
public class SensorService
{
    /// <summary>
    /// Map from SensorData to the correct sensor
    /// </summary>
    private readonly Dictionary<Type, object> _sensors = [];

    /// <summary>
    /// Register new sensor
    /// </summary>
    /// <param name="sensor">sensor</param>
    /// <typeparam name="TData">datatype from sensor</typeparam>
    public void Register<TData>(ISensor<TData> sensor) where TData : notnull
    {
        if (!_sensors.ContainsKey(typeof(TData)))
        {
            _sensors[typeof(TData)] = sensor;
        }
    }

    /// <summary>
    /// Get sensor from the datatype
    /// </summary>
    /// <typeparam name="TData">datatype</typeparam>
    /// <returns>sensor</returns>
    public ISensor<TData>? Get<TData>() where TData : notnull
    {
        if (_sensors.ContainsKey(typeof(TData)))
        {
            return (ISensor<TData>) _sensors[typeof(TData)];
        }

        return null;
    }
}