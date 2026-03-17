using PflanzenPi.Sensor.Sensors;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

public class LittleLight : IBrightnessBehaviour
{
    public BrightnessStatus Interpret(Brightness brightness)
    {
        if (brightness < 150)
        {
            return BrightnessStatus.VeryLow;
        }

        if (brightness < 300)
        {
            return BrightnessStatus.Low;
        }

        if (brightness < 550)
        {
            return BrightnessStatus.Satisfied;
        }

        return brightness < 700 ? BrightnessStatus.Medium : BrightnessStatus.High;
    }
}