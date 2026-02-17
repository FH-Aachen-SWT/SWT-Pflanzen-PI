using PflanzenPi.Sensor;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

public class MediumLight : IBrightnessBehaviour
{
    public BrightnessStatus Interpret(Brightness brightness)
    {
        //TODO Echte werte nehmen
        if (brightness < 30)
        {
            return BrightnessStatus.VeryLow;
        }

        if (brightness < 35)
        {
            return BrightnessStatus.Low;
        }

        if (brightness < 45)
        {
            return BrightnessStatus.Satisfied;
        }

        return brightness < 50 ? BrightnessStatus.Medium : BrightnessStatus.High;
    }
}