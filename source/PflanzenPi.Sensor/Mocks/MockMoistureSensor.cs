namespace PflanzenPi.Sensor.Mocks;

public class MockMoistureSensor : Sensor<Moisture>
{
    private readonly Timer _timer;

    public MockMoistureSensor()
    {
        _timer = new Timer(SimuliereLesen, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(5));
    }
    
    private void SimuliereLesen(object? _)
    {
        var moisture = (float) Random.Shared.NextDouble() * 100f;
        Publish(new Moisture(moisture));
    }
}