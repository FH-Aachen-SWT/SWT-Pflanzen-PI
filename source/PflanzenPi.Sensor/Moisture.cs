namespace PflanzenPi.Sensor;

public class Moisture : SensorData<float>
{
    public Moisture(float data) : base(data)
    {
        if (data < 0 || data > 1000)
        {
            throw new ArgumentOutOfRangeException("Keinen gültigen Wert von der PI gelesen");
        }
    }
}