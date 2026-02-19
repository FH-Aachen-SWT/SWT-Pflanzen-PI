namespace PflanzenPi.Sensor.Adapter;

public interface II2CAdapter
{
    public short ReadRawShort();

    public double ReadRawDouble();
}