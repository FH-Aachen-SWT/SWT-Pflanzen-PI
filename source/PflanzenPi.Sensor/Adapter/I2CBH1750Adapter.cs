using System.Device.I2c;
using Iot.Device.Bh1750fvi;

namespace PflanzenPi.Sensor.Sensors.Adapter;

public class I2CBH1750Adapter : II2CAdapter
{
    private readonly Bh1750fvi _sensor;

    public I2CBH1750Adapter(Bh1750fvi? sensor = null)
    {
        _sensor = sensor ?? CreateDefault();
    }
    
    private Bh1750fvi CreateDefault()
    {
        I2cConnectionSettings config = new I2cConnectionSettings(1,0x23);
        I2cDevice device = I2cDevice.Create(config);
        return new Bh1750fvi(device);
    }

    public short ReadRawShort()
    {
        throw new NotImplementedException();
    }

    public double ReadRawDouble()
    {
        return _sensor.Illuminance.Lux;
    }
}