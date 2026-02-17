using System.Device.I2c;
using Iot.Device.Ads1115;

namespace PflanzenPi.Sensor;

/// <summary>
/// Moisture sensor, Sensor with moisture as datatype
/// </summary>
public class MoistureSensor : Sensor<Moisture>
{
    private readonly Timer _timer;
    private readonly Ads1115 _adc;
    private const float MaximumVoltage = 3.3f;
    private const float Gain = 4.096f;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="interval">Interval in which data is to be read</param>
    public MoistureSensor(TimeSpan interval)
    {
        I2cConnectionSettings config = new I2cConnectionSettings(1,0x48);
        I2cDevice device = I2cDevice.Create(config);
        _adc = new Ads1115(device);
        _timer = new Timer(ReadFromPi , null, TimeSpan.FromSeconds(0), interval);
    }
        
    /// <summary>
    /// Reads raw data from Raspberry PI
    /// </summary>
    /// <returns>Raw data converted to moisture</returns>
    private void ReadFromPi(Object? _)
    {
        var raw = _adc.ReadRaw();
        float percentage = raw * (Gain / Int16.MaxValue) * (100.0f / MaximumVoltage);
        Moisture moisture = new Moisture(percentage);
        Publish(moisture);
        Console.WriteLine($"RAW: {raw} PER: {percentage}%");
    }
}