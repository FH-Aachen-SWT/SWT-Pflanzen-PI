using System.Device.I2c;
using Iot.Device.Ads1115;
using PflanzenPi.Sensor.Sensors.Adapter;

namespace PflanzenPi.Sensor.Sensors;

public class BrightnessSensor : Sensor<Brightness>
{
    private readonly Timer _timer;
    private readonly II2CAdapter _adc; // custom Interface zur Typkapselung und um Mocking zu ermöglichen
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="interval">Interval in which data is to be read</param>
    /// <param name="adc">For test purposes the adc can be injected, default is null</param>
    public BrightnessSensor(TimeSpan interval, II2CAdapter? adc = null)
    {
        _adc = adc ?? new I2CBH1750Adapter();
        _timer = new Timer(ReadFromPi , null, TimeSpan.FromSeconds(0), interval);
    }
        
    /// <summary>
    /// Reads raw data from Raspberry PI and converts to brightness
    /// </summary>
    /// <returns>Raw data converted to brightness</returns>
    private void ReadFromPi(Object? _)
    {
        var raw = _adc.ReadRawDouble();
        
        Brightness brightness = new Brightness(raw);
        Publish(brightness);
        Console.WriteLine($"LUX: {raw}");
    }
}