using System.Device.I2c;
using Iot.Device.Ads1115;
using PflanzenPi.Sensor.Sensors.Adapter;

namespace PflanzenPi.Sensor.Sensors;

/// <summary>
/// Moisture sensor, Sensor with moisture as datatype
/// </summary>
public class MoistureSensor : Sensor<Moisture>
{
    private readonly Timer _timer;
    private readonly II2CAdapter _adc; // custom Interface zur Typkapselung und um Mocking zu ermöglichen
    private const float MaximumVoltage = 3.3f;
    private const float Gain = 4.096f;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="interval">Interval in which data is to be read</param>
    /// <param name="adc">For test purposes the adc can be injected, default is null</param>
    public MoistureSensor(TimeSpan interval, II2CAdapter? adc = null)
    {
        _adc = adc ?? new I2CAdcAdapter();
        _timer = new Timer(ReadFromPi , null, TimeSpan.FromSeconds(0), interval);
    }
        
    /// <summary>
    /// Reads raw data from Raspberry PI and converts to moisture
    /// </summary>
    /// <returns>Raw data converted to moisture</returns>
    private void ReadFromPi(Object? _)
    {
        var raw = _adc.ReadRawShort();
        float percentage = raw * (Gain / (Int16.MaxValue + 1)) * (100.0f / MaximumVoltage); // Lieber Int16.MaxValue + 1 da Int16.MaxValue als 32767 definiert ist aber ADS115 technisch mit 32768 arbeitet.
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
        float calibratedPercentage = Math.Clamp(-(100/43)*(percentage-67),0f,100f);
        Moisture moisture = new Moisture(calibratedPercentage);
        Publish(moisture);
    }
}