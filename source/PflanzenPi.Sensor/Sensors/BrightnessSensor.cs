using System.Device.I2c;
using Iot.Device.Ads1115;

namespace PflanzenPi.Sensor;

public class BrightnessSensor : Sensor<Brightness>
{
    private readonly Timer _timer;
    private readonly Ads1115 _adc;
    private const float MaximumVoltage = 3.3f;
    private const float Gain = 4.096f;

    public BrightnessSensor(TimeSpan interval)
    {
        I2cConnectionSettings config = new I2cConnectionSettings(2,0x48);
        I2cDevice device = I2cDevice.Create(config);
        _adc = new Ads1115(device);
        _timer = new Timer(ReadFromPi , null, TimeSpan.FromSeconds(0), interval);
    }
    
    /// <summary>
    /// Reads raw data from Raspberry PI
    /// </summary>
    /// <returns>Raw data converted to moisture</returns>
    private void ReadFromPi(Object? _)
    {
        var raw = _adc.ReadRaw();
        float percentage = raw * (Gain / Int16.MaxValue + 1) * (100.0f / MaximumVoltage); // Lieber Int16.MaxValue + 1 da Int16.MaxValue als 32767 definiert ist aber ADS115 technisch mit 32768 arbeitet.
        /*
         * Bei Kalibrierung des Sensors ergab sich:
         * Völlige Nässe -> 24%
         * Völlige Trockenheit -> 67%
         *
         * Um die Werte benutzbar zu machen, bilde ich sie linear umgedreht auf den Zahlenraum von 0-100 ab.
         * Die neu kalibrierten Werte sehen wie folgt aus:
         * Völlige Nässe -> 100%
         * Völlige Trockenheit -> 0%
         *
         * Abbildung: f(x)= -(100/(67-24))*(x-67)
         */
        float calibratedPercentage = -(100/43)*(percentage-67);
        Brightness brightness = new Brightness(calibratedPercentage);
        Publish(brightness);
        Console.WriteLine($"RAW: {raw} PER: {calibratedPercentage}%");
    }
}