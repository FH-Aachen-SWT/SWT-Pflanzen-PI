namespace sensor;

public class MoistureSensor : Sensor<Moisture>
{
    private readonly Timer _timer;

    public MoistureSensor()
    {
        _timer = new Timer(SimuliereLesen, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1));
    }
    
    public void SimuliereLesen(object? _)
    {
        float moisture = (float)Random.Shared.NextDouble() * 100f;
        Publish(new Moisture(moisture));
    }
}