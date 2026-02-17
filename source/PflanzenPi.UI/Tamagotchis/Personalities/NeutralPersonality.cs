using System;
using PflanzenPi.UI.Tamagotchis.Moods;
using static PflanzenPi.UI.Tamagotchis.Moods.Mood;

namespace PflanzenPi.UI.Tamagotchis.Personalities;

/// <summary>
/// Neutral personality
/// </summary>
public class NeutralPersonality : IPersonality
{
    /// <inheritdoc/>
    public string ProvideImage(Mood mood)
    {
        return mood switch
        {
            Happy => "satisfied.png",
            Neutral => "drySad.png",
            Sad =>  "wetNeutral.png",
            Angry => "dryWetAngry.png",
            _ => throw new ArgumentOutOfRangeException(nameof(mood), mood, null)
        };
    }
}