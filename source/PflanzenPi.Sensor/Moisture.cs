namespace PflanzenPi.Sensor;

public class Moisture : SensorData<float>
{
    public float Data;

    public Moisture(float data)
    {
        if (data < 0 || data > 1000)
        {
            throw new ArgumentOutOfRangeException("Keinen gültigen Wert von der PI gelesen");
        }
    }
}