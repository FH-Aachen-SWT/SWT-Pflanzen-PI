using System.Diagnostics;

namespace PflanzenPi.Sensor.Mocks;

/// <summary>
/// Mock for the moisture sensor
/// </summary>
public class MockMoistureSensor : Sensor<Moisture>
{
    private readonly Timer _timer;

    public MockMoistureSensor()
    {
        _timer = new Timer(SimuliereLesen, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1));
    }
    
    private void SimuliereLesen(object? _)
    {
        var moisture = (float) Random.Shared.NextDouble() * 100f;
        Publish(new Moisture(moisture));
        Console.WriteLine($"MOCK READ MOISTURE:  {moisture}");
    }
}