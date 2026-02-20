using PflanzenPi.Sensor.Sensors;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

public class MediumLight : IBrightnessBehaviour
{
    public BrightnessStatus Interpret(Brightness brightness)
    {
        if (brightness < 4)
        {
            return BrightnessStatus.VeryLow;
        }

        if (brightness < 6)
        {
            return BrightnessStatus.Low;
        }

        if (brightness < 10)
        {
            return BrightnessStatus.Satisfied;
        }

        return brightness < 14 ? BrightnessStatus.Medium : BrightnessStatus.High;
    }
}