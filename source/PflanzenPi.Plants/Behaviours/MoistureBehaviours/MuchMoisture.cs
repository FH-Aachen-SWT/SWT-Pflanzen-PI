using Iot.Device.Gpio.Drivers;
using PflanzenPi.Sensor.Sensors;

namespace PflanzenPi.Plants.Behaviours.MoistureBehaviours;

/// <summary>
/// Plants that need a moisture level between 55% and 65%
/// </summary>
public class MuchMoisture : IMoistureBehaviour
{
    /// <inheritdoc/>
    public MoistureStatus Interpret(Moisture moisture)
    {
        if (moisture < GetLowestMoistureThreashold())
        {
            return MoistureStatus.VeryDry;
        }

        if (moisture < 55)
        {
            return MoistureStatus.Dry;
        }

        if (moisture < 65)
        {
            return MoistureStatus.Satisfied;
        }

        return moisture < 70 ? MoistureStatus.Wet : MoistureStatus.VeryWet;
    }

    public Moisture GetLowestMoistureThreashold()
    {
        return 50;
    }
}