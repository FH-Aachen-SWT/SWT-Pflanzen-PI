using PflanzenPi.Sensor.Sensors;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

public class MuchLight : IBrightnessBehaviour
{
    public BrightnessStatus Interpret(Brightness brightness)
    {
        if (brightness < 10)
        {
            return BrightnessStatus.VeryLow;
        }

        if (brightness < 14)
        {
            return BrightnessStatus.Low;
        }

        if (brightness < 18)
        {
            return BrightnessStatus.Satisfied;
        }

        return brightness < 24 ? BrightnessStatus.Medium : BrightnessStatus.High;
    }
}