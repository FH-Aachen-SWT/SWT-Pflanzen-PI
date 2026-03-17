using PflanzenPi.Sensor.Sensors;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

public class MuchLight : IBrightnessBehaviour
{
    public BrightnessStatus Interpret(Brightness brightness)
    {
        if (brightness < 450)
        {
            return BrightnessStatus.VeryLow;
        }

        if (brightness < 700)
        {
            return BrightnessStatus.Low;
        }

        if (brightness < 1100)
        {
            return BrightnessStatus.Satisfied;
        }

        return brightness < 1350 ? BrightnessStatus.Medium : BrightnessStatus.High;
    }
}