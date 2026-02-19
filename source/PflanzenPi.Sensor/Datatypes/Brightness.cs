namespace PflanzenPi.Sensor;

/// <summary>
/// Sensordata describing the Brightness from 0 to 100 percent
/// </summary>
public class Brightness : SensorData<double>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Brightness(double data) : base(data)
    {
        if (data < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(data), "Muss grösser 0 sein!");
        }
    }
}