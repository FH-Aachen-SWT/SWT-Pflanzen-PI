namespace PflanzenPi.Sensor;

public class SensorService
{
    /// <summary>
    /// Map von SensorDaten auf den dafür angelegten Sensor
    /// </summary>
    private readonly Dictionary<Type, object> _sensors = [];

    /// <summary>
    /// Registriere neuen Sensor 
    /// </summary>
    /// <param name="sensor"></param>
    /// <typeparam name="TData"></typeparam>
    public void Register<TData>(ISensor<TData> sensor) where TData : notnull
    {
        if (!_sensors.ContainsKey(typeof(TData)))
        {
            _sensors[typeof(TData)] = sensor;
        }
    }

    /// <summary>
    /// Hole Sensor von dem Typen
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <returns></returns>
    public ISensor<TData>? Get<TData>() where TData : notnull
    {
        if (_sensors.ContainsKey(typeof(TData)))
        {
            return (ISensor<TData>) _sensors[typeof(TData)];
        }

        return null;
    }
}