using PflanzenPI.Persistence.Business;

namespace PflanzenPi.Sensor.Sensors.Mocks;

public class MockBrightnessSensorSine : Sensor<Brightness>
{
    private readonly Timer _timer;

    private float pos = 4.79f; // Offset damit Brightness und Moisture nicht gleich sind beim mocken

    private readonly float _increment = 0.25f;

    private readonly float _middle;

    private readonly IBrightnessService _service;

    private int currentHour = 1;
    private double currentLuxSum = 0;
    private int currentNumOfReadings = 0;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="perfectBrightnessLevel">Middle brightness level wanted for simulation</param>
    public MockBrightnessSensorSine(int perfectBrightnessLevel, TimeSpan interval, IBrightnessService service)
    {
        _service = service;
        _middle = (float) perfectBrightnessLevel / 10000;
        _timer = new Timer(SimuliereLesen, null, TimeSpan.FromSeconds(0), interval);
    }
    
    /// <summary>
    /// Simulate read with sine function
    /// </summary>
    /// <param name="_"></param>
    private async void SimuliereLesen(object? _)
    {
        getNextPos();
        var brightnessRaw = (float) (3*Math.Sin(pos) + _middle) * 10000;
        var now = DateTime.Now;
        if (now.Hour == currentHour)
        {
            currentHour++;
            currentLuxSum = 0;
            currentNumOfReadings = 0;
        }

        currentLuxSum += brightnessRaw;
        currentNumOfReadings++;

        var avgForHour = currentLuxSum / currentNumOfReadings;
        await _service.SetBrightnessForHourAsync(currentHour, avgForHour);

        var newDil = await _service.GetDLIAsync();
        
        Brightness brightness = new Brightness(newDil);
        Console.WriteLine($"New DLI: {newDil}");
        Publish(brightness);
    }

    /// <summary>
    /// Next position to give sine function
    /// </summary>
    private void getNextPos()
    {
        pos += _increment;
        if (pos < 0)
        {
            pos = 0;
        }
    }
}