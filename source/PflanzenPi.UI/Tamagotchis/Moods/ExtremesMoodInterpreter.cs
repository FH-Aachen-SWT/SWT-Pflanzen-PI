using System;
using PflanzenPi.Plants;

namespace PflanzenPi.UI.Tamagotchis.Moods;

public class ExtremesMoodInterpreter : IMoodInterpreter
{
    public Mood Interpret(MoistureStatus moisture, BrightnessStatus brightness)
    {
        var moist = Math.Abs((int)MoistureStatus.Satisfied - (int)moisture);
        var bright = Math.Abs((int)BrightnessStatus.Satisfied - (int)brightness);

        var max = moist > bright ? moist : bright;

        if ((max == 2 && (moist == 1 || bright == 1)) || (bright == 2 && moist == 2))
            return Mood.Angry;
        
        return max switch
        {
            0 => Mood.Happy,
            1 => Mood.Neutral,
            2 => Mood.Sad,
            _ => throw new ArgumentOutOfRangeException("Einer der Parameter war ungültig")
        };
    }
}