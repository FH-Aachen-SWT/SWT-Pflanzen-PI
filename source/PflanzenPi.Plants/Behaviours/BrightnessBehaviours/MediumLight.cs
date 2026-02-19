using PflanzenPi.Sensor.Sensors;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

public class MediumLight : IBrightnessBehaviour
{
    public BrightnessStatus Interpret(Brightness brightness)
    {
        if (brightness < 5000)
        {
            return BrightnessStatus.VeryLow;
        }

        if (brightness < 20000)
        {
            return BrightnessStatus.Low;
        }

        if (brightness < 45000)
        {
            return BrightnessStatus.Satisfied;
        }

        return brightness < 55000 ? BrightnessStatus.Medium : BrightnessStatus.High;
    }
}