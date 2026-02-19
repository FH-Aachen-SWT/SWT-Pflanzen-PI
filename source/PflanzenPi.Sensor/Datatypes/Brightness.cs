namespace PflanzenPi.Sensor;

/// <summary>
/// Sensordata describing the Brightness from 0 to 100 percent
/// </summary>
public class Brightness : SensorData<float>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Brightness(float data) : base(data)
    {
        if (data < 0 || data > 100.0f)
        {
            throw new ArgumentOutOfRangeException(nameof(data), "Muss zwischen 0 und 100 % liegen");
        }
    }
}