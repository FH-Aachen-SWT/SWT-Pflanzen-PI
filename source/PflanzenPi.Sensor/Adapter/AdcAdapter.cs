using System.Device.I2c;
using Iot.Device.Ads1115;

namespace PflanzenPi.Sensor.Adapter;

public class AdcAdapter : IAdcAdapter
{
    private readonly Ads1115 _adc;

    public AdcAdapter(Ads1115? adc = null)
    {
        _adc = adc ?? CreateDefaultAdc();
    }
    
    public short ReadRaw()
    {
        return _adc.ReadRaw();
    }

    private Ads1115 CreateDefaultAdc()
    {
        I2cConnectionSettings config = new I2cConnectionSettings(1,0x48);
        I2cDevice device = I2cDevice.Create(config);
        return new Ads1115(device);
    }
}