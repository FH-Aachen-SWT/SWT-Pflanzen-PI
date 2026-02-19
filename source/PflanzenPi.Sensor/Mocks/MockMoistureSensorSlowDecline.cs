using System.Diagnostics;

namespace PflanzenPi.Sensor.Sensors.Mocks;

/// <summary>
/// Mock for the moisture sensor which slowly declines to simulate realism
/// It "refills" the water once it gets low enough
/// </summary>
public class MockMoistureSensorSlowDecline : Sensor<Moisture>
{
    private readonly Timer _timer;
    private readonly TimeSpan _interval;
    private int steps;

    public MockMoistureSensorSlowDecline(TimeSpan interval)
    {
        _interval = interval;
        steps = 0;
        _timer = new Timer(SimuliereLesen, null, TimeSpan.FromSeconds(0), interval);
    }
    
    private void SimuliereLesen(object? _)
    {
        var startingValue = 90.0f;
        var averageDecline = 0.05f; // average decline per second
        var rauschen = (float)Random.Shared.NextDouble() - 0.5f; // Zufälliger Wert von -0,5 bis 0.5 
        var rauschenIntensity = 2 * averageDecline; // Vorfaktor vom Rauschen; should always be chosen relative to the averageDecline and idealy bigger than the average decline so that a move up is possible through disturbance
        var moisture = startingValue - steps * averageDecline * (float)_interval.TotalSeconds + (rauschen * rauschenIntensity);
        if (moisture < 28.0f) // refill
        {
            steps = 0;
        }
        else
        {
            steps++;
        }
        Publish(new Moisture(moisture));
        Console.WriteLine($"MOCK READ MOISTURE:  {moisture}");
    }
}