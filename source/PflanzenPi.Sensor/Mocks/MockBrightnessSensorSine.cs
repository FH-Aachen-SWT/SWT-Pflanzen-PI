namespace PflanzenPi.Sensor.Sensors.Mocks;

public class MockBrightnessSensorSine : Sensor<Brightness>
{
    private readonly Timer _timer;

    private float pos = 4.79f; // Offset damit Brightness und Moisture nicht gleich sind beim mocken

    private readonly float _increment = 0.25f;

    private readonly float _middle;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="perfectBrightnessLevel">Middle brightness level wanted for simulation</param>
    public MockBrightnessSensorSine(int perfectBrightnessLevel, TimeSpan interval)
    {
        _middle = (float) perfectBrightnessLevel / 10000;
        _timer = new Timer(SimuliereLesen, null, TimeSpan.FromSeconds(0), interval);
    }
    
    /// <summary>
    /// Simulate read with sine function
    /// </summary>
    /// <param name="_"></param>
    private void SimuliereLesen(object? _)
    {
        getNextPos();
        var brightness = (float) (3*Math.Sin(pos) + _middle) * 10000;
        Publish(new Brightness(brightness));
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