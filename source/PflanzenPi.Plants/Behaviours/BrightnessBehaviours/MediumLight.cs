using PflanzenPi.Sensor.Sensors;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

public class MediumLight : IBrightnessBehaviour
{
    public BrightnessStatus Interpret(Brightness brightness)
    {
        if (brightness < 350)
        {
            return BrightnessStatus.VeryLow;
        }

        if (brightness < 550)
        {
            return BrightnessStatus.Low;
        }

        if (brightness < 800)
        {
            return BrightnessStatus.Satisfied;
        }

        return brightness < 1000 ? BrightnessStatus.Medium : BrightnessStatus.High;
    }
}