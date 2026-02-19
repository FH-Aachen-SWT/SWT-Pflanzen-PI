using System.Device.I2c;
using Iot.Device.Ads1115;

namespace PflanzenPi.Sensor.Adapter;

public class I2CAdcAdapter : II2CAdapter
{
    private readonly Ads1115 _adc;

    public I2CAdcAdapter(Ads1115? adc = null)
    {
        _adc = adc ?? CreateDefaultAdc();
    }
    
    public short ReadRawShort()
    {
        return _adc.ReadRaw();
    }

    public double ReadRawDouble()
    {
        throw new NotImplementedException();
    }

    private Ads1115 CreateDefaultAdc()
    {
        I2cConnectionSettings config = new I2cConnectionSettings(1,0x48);
        I2cDevice device = I2cDevice.Create(config);
        return new Ads1115(device);
    }
}