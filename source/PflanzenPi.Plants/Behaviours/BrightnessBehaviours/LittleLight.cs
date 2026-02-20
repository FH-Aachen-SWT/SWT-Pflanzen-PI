using PflanzenPi.Sensor.Sensors;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

public class LittleLight : IBrightnessBehaviour
{
    public BrightnessStatus Interpret(Brightness brightness)
    {
        if (brightness < 1)
        {
            return BrightnessStatus.VeryLow;
        }

        if (brightness < 2)
        {
            return BrightnessStatus.Low;
        }

        if (brightness < 6)
        {
            return BrightnessStatus.Satisfied;
        }

        return brightness < 8 ? BrightnessStatus.Medium : BrightnessStatus.High;
    }
}