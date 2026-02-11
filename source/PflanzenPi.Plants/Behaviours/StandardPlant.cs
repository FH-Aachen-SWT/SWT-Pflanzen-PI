using PflanzenPi.Sensor;

namespace PflanzenPi.Plants.Behaviours;

public class StandardPlant : IMoistureBehaviour
{
    public MoistureStatus Interpret(Moisture moisture)
    {
        if (moisture <= 0)
        {
            return MoistureStatus.VeryDry;
        }

        if (moisture <= 30)
        {
            return MoistureStatus.Satisfied;
        }
        return  MoistureStatus.VeryWet;
    }
}