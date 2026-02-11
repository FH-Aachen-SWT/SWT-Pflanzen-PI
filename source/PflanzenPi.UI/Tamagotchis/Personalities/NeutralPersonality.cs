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
            Happy => "pflanzi.png",
            Neutral => string.Empty,
            Sad => string.Empty,
            Angry => string.Empty,
            _ => throw new ArgumentOutOfRangeException(nameof(mood), mood, null)
        };
    }
}