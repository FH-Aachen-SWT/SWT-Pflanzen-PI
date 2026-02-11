using System;
using PflanzenPi.Plants;
using static PflanzenPi.Plants.MoistureStatus;
using static PflanzenPi.UI.Tamagotchis.Moods.Mood;

namespace PflanzenPi.UI.Tamagotchis.Moods;

public class DefaultMoodInterpreter : IMoodInterpreter
{
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