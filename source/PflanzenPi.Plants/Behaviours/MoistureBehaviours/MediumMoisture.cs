using PflanzenPi.Sensor.Sensors;

namespace PflanzenPi.Plants.Behaviours.MoistureBehaviours;

/// <summary>
/// Plants that need a moisture level between 35% and 45%
/// </summary>
public class MediumMoisture : IMoistureBehaviour
{
    
    /// <inheritdoc/>
    public MoistureStatus Interpret(Moisture moisture)
    {
        if (moisture < GetLowestMoistureThreashold())
        {
            return MoistureStatus.VeryDry;
        }

        if (moisture < 35)
        {
            return MoistureStatus.Dry;
        }

        if (moisture < 45)
        {
            return MoistureStatus.Satisfied;
        }

        return moisture < 50 ? MoistureStatus.Wet : MoistureStatus.VeryWet;
    }

    public Moisture GetLowestMoistureThreashold()
    {
        return 30;
    }
}