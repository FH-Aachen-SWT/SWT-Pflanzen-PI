using PflanzenPi.Sensor;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

public class MuchLight : IBrightnessBehaviour
{
    public BrightnessStatus Interpret(Brightness brightness)
    {
        if (brightness < 50)
        {
            return BrightnessStatus.VeryLow;
        }

        if (brightness < 55)
        {
            return BrightnessStatus.Low;
        }

        if (brightness < 65)
        {
            return BrightnessStatus.Satisfied;
        }

        return brightness < 70 ? BrightnessStatus.Medium : BrightnessStatus.High;
    }
}