namespace PflanzenPi.Sensor.Sensors;

/// <summary>
/// Data type for soil moisture levels
/// </summary>
public class Moisture : SensorData<float>
{
    /// <summary>
    /// Checks argument and converts to percent
    /// </summary>
    /// <param name="data">raw data</param>
    /// <exception cref="ArgumentOutOfRangeException">Argument out of range exception if not between 0 and 100</exception>
    public Moisture(float data) : base(data)
    {
        if (data < 0 || data > 100.0f)
        {
            throw new ArgumentOutOfRangeException(nameof(data), "Keinen gültigen Wert von der PI gelesen");
        }
    }
    
    /// <summary>
    /// Implicit conversion from raw datatype to SensorData
    /// </summary>
    /// <param name="sensorData">SensorData</param>
    /// <returns>raw datatype</returns>
    public static implicit operator Moisture(float sensorData)
    {
        return new Moisture(sensorData);
    }

}