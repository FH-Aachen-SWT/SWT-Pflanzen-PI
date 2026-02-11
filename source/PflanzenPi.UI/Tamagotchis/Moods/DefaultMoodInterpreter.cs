using System;
using PflanzenPi.Plants;
using static PflanzenPi.Plants.MoistureStatus;
using static PflanzenPi.UI.Tamagotchis.Moods.Mood;

namespace PflanzenPi.UI.Tamagotchis.Moods;

/// <summary>
/// Default mood interpreter
/// </summary>
public class DefaultMoodInterpreter : IMoodInterpreter
{
    /// <inheritdoc/>
    public Mood Interpret(MoistureStatus moisture)
    {
        return moisture switch
        {
            VeryDry => Angry,
            Dry => Neutral,
            Satisfied => Happy,
            Wet => Sad,
            VeryWet => Angry,
            _ => throw new ArgumentOutOfRangeException(nameof(moisture), moisture, null)
        };
    }
}