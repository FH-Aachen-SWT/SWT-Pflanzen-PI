namespace sensor;

public sealed class Moisture : SensorData<float>
{
    public Moisture(float value) : base(value)
    {
        //Hier min max checks
    }
}