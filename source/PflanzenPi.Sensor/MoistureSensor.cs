namespace PflanzenPi.Sensor;

// Muss noch Singleton sein
public class MoistureSensor : Sensor<Moisture>
{
    private readonly Timer _timer;

    public MoistureSensor(TimeSpan interval)
    {
        
    }

    private Moisture ReadFromPI()
    {
        return null;
    }
}