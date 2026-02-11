namespace PflanzenPi.Sensor;

/// <summary>
/// Moisture sensor, Sensor with moisture as datatype
/// </summary>
public class MoistureSensor : Sensor<Moisture>
{
    private readonly Timer _timer;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="interval">Interval in which data is to be read</param>
    public MoistureSensor(TimeSpan interval)
    {
        
    }

    /// <summary>
    /// Reads raw data from Raspberry PI
    /// </summary>
    /// <returns>Raw data converted to moisture</returns>
    private Moisture ReadFromPI()
    {
        return null;
    }
}