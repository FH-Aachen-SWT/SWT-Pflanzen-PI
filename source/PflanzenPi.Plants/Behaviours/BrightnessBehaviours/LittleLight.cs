using PflanzenPi.Sensor;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

public class LittleLight : IBrightnessBehaviour
{
    public BrightnessStatus Interpret(Brightness brightness)
    {
        //TODO Echte werte nehmen
        if (brightness < 10)
        {
            return BrightnessStatus.VeryLow;
        }

        if (brightness < 15)
        {
            return BrightnessStatus.Low;
        }

        if (brightness < 25)
        {
            return BrightnessStatus.Satisfied;
        }

        return brightness < 30 ? BrightnessStatus.Medium : BrightnessStatus.High;
    }
}