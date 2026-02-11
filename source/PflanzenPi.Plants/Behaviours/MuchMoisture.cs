using PflanzenPi.Sensor;

namespace PflanzenPi.Plants.Behaviours;

/// <summary>
/// Plants that need a moisture level between 55% and 65%
/// </summary>
public class MuchMoisture : IMoistureBehaviour
{
    /// <inheritdoc/>
    public MoistureStatus Interpret(Moisture moisture)
    {
        if (moisture < 50)
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
}