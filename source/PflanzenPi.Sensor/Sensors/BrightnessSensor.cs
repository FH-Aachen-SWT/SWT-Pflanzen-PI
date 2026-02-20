using System.Device.I2c;
using Iot.Device.Ads1115;
using PflanzenPI.Persistence.Business;
using PflanzenPi.Sensor.Sensors.Adapter;

namespace PflanzenPi.Sensor.Sensors;

public class BrightnessSensor : Sensor<Brightness>
{
    private readonly Timer _timer;
    private readonly II2CAdapter _adc; // custom Interface zur Typkapselung und um Mocking zu ermöglichen
    private readonly IBrightnessService _service;

    private int currentHour = 1;
    private double currentLuxSum = 0;
    private int currentNumOfReadings = 0;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="interval">Interval in which data is to be read</param>
    /// <param name="brightnessService"></param>
    /// <param name="adc">For test purposes the adc can be injected, default is null</param>
    public BrightnessSensor(TimeSpan interval, IBrightnessService brightnessService, II2CAdapter? adc = null)
    {
        _service = brightnessService;
        _adc = adc ?? new I2CBH1750Adapter();
        _timer = new Timer(ReadFromPi , null, TimeSpan.FromSeconds(0), interval);
    }
        
    /// <summary>
    /// Reads raw data from Raspberry PI and converts to brightness
    /// </summary>
    /// <returns>Raw data converted to brightness</returns>
    private async void ReadFromPi(Object? _)
    {
        var raw = _adc.ReadRawDouble();

        var now = DateTime.Now;
        if (now.Hour == currentHour)
        {
            currentHour++;
            currentLuxSum = 0;
            currentNumOfReadings = 0;
        }

        currentLuxSum += raw;
        currentNumOfReadings++;

        var avgForHour = currentLuxSum / currentNumOfReadings;
        await _service.SetBrightnessForHourAsync(currentHour, avgForHour);

        var newDil = await _service.GetDLIAsync();
        
        Brightness brightness = new Brightness(newDil);
        Console.WriteLine($"New DLI: {newDil}");
        Publish(brightness);
    }
}