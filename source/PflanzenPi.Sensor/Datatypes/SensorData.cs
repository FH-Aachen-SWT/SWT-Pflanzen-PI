namespace PflanzenPi.Sensor;

/// <summary>
/// Datatype for converted data from sensors
/// </summary>
/// <typeparam name="T">Raw datatype</typeparam>
public abstract class SensorData<T>
{
    public T Data { get; }

    /// <summary>
    /// Constructor without checks or conversions
    /// </summary>
    /// <param name="data">Raw data</param>
    public SensorData(T data)
    {
        Data = data;
    }

    /// <summary>
    /// Implicit conversion from SensorData to raw datatype
    /// </summary>
    /// <param name="sensorData">SensorData</param>
    /// <returns>raw datatype</returns>
    public static implicit operator T(SensorData<T> sensorData)
    {
        return sensorData.Data;
    }
    
}