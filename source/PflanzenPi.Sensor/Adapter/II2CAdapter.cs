namespace PflanzenPi.Sensor.Sensors.Adapter;

public interface II2CAdapter
{
    public short ReadRawShort();

    public double ReadRawDouble();
}