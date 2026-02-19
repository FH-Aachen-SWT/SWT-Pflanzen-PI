using System;
using PflanzenPi.Plants;
using static PflanzenPi.Plants.MoistureStatus;
using static PflanzenPi.UI.Tamagotchis.Moods.Mood;

namespace PflanzenPi.UI.Tamagotchis.Moods;

/// <summary>
/// Default mood interpreter
/// </summary>
public class UnweightedMoodInterpreter : IMoodInterpreter
{
    /// <inheritdoc/>
    public Mood Interpret(MoistureStatus moisture, BrightnessStatus brightness)
    {
        var avg = ((double)moisture + (double)brightness) / 2;
        return avg switch
        {
            < 0.5 or > 3.5 => Angry,
            < 1 or > 3 => Sad,
            < 1.5 or > 2.5 => Neutral,
            _ => Happy
        };
    }
}