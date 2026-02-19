using PflanzenPi.Sensor;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

public class LittleLight : IBrightnessBehaviour
{
    public BrightnessStatus Interpret(Brightness brightness)
    {
        if (brightness < 500)
        {
            return BrightnessStatus.VeryLow;
        }

        if (brightness < 1500)
        {
            return BrightnessStatus.Low;
        }

        if (brightness < 2500)
        {
            return BrightnessStatus.Satisfied;
        }

        return brightness < 3000 ? BrightnessStatus.Medium : BrightnessStatus.High;
    }
}