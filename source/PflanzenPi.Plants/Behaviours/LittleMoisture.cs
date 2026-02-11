using PflanzenPi.Sensor;

namespace PflanzenPi.Plants.Behaviours;

/// <summary>
/// Plants that need a moisture level between 15% and 25%
/// </summary>
public class LittleMoisture : IMoistureBehaviour
{
    /// <inheritdoc/>
    public MoistureStatus Interpret(Moisture moisture)
    {
        if (moisture < 10)
        {
            return MoistureStatus.VeryDry;
        }

        if (moisture < 15)
        {
            return MoistureStatus.Dry;
        }

        if (moisture < 25)
        {
            return MoistureStatus.Satisfied;
        }

        return moisture < 30 ? MoistureStatus.Wet : MoistureStatus.VeryWet;
    }
}