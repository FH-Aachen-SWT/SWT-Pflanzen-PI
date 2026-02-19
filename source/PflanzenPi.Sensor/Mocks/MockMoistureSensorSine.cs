namespace PflanzenPi.Sensor.Sensors.Mocks;

/// <summary>
/// Mock moisture sensor using Sine Wave
/// </summary>
public class MockMoistureSensorSine : Sensor<Moisture>
{
    private readonly Timer _timer;

    private float pos = 0;

    private readonly float _increment = 0.25f;

    private readonly float _middle;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="perfectMoistureLevel">Middle moisture level wanted for simulation</param>
    public MockMoistureSensorSine(int perfectMoistureLevel, TimeSpan interval)
    {
        _middle = (float) perfectMoistureLevel / 100;
        _timer = new Timer(SimuliereLesen, null, TimeSpan.FromSeconds(0), interval);
    }
    
    /// <summary>
    /// Simulate read with sine function
    /// </summary>
    /// <param name="_"></param>
    private void SimuliereLesen(object? _)
    {
        getNextPos();
        var moisture = (float) (0.12*Math.Sin(pos) + _middle) * 100;
        Publish(new Moisture(moisture));
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