using PflanzenPi.Plants;
using static PflanzenPi.UI.Tamagotchis.Moods.Mood;

namespace PflanzenPi.UI.Tamagotchis.Moods;

public class WeightedMoodInterpreter: IMoodInterpreter
{
    private readonly double _moistureWeight = 1.3;
    private readonly double _brightnessWeight = 0.8;
    
    public Mood Interpret(MoistureStatus moisture, BrightnessStatus brightness)
    {
        var avg = ((int)moisture*_moistureWeight + (int)brightness*_brightnessWeight)/2;
        return avg switch
        {
            < 0.5 or > 3.5 => Angry,
            < 1 or > 3 => Sad,
            < 1.5 or > 2.5 => Neutral,
            _ => Happy
        };
    }
}