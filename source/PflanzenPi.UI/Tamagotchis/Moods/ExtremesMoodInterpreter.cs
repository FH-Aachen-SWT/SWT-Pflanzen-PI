using System;
using PflanzenPi.Plants;

namespace PflanzenPi.UI.Tamagotchis.Moods;

public class ExtremesMoodInterpreter : IMoodInterpreter
{
    public Mood Interpret(MoistureStatus moisture, BrightnessStatus brightness)
    {
        var moist = Math.Abs(2 - (int)moisture);
        var bright = Math.Abs(2 - (int)brightness);

        var max = moist > bright ? (int)moisture : (int)brightness;

        return max switch
        {
            0 => Mood.Angry,
            1 => Mood.Neutral,
            2 => Mood.Happy,
            3 => Mood.Sad,
            4 => Mood.Angry,
            _ => throw new ArgumentOutOfRangeException("Einer der Parameter war ungültig")
        };
    }
}