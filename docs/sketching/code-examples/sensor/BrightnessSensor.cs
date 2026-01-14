namespace sensor;

public class BrightnessSensor : Sensor<Brightness>
{
    private readonly Timer _timer;

    public BrightnessSensor()
    {
        _timer = new Timer(SimuliereLesen, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1));
    }
    
    public void SimuliereLesen(object? _)
    {
        int brightness = Random.Shared.Next(100,600);
        Publish(new Brightness(brightness));
    }
}