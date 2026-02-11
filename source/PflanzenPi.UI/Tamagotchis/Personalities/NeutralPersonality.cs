using System;
using PflanzenPi.UI.Tamagotchis.Moods;
using static PflanzenPi.UI.Tamagotchis.Moods.Mood;

namespace PflanzenPi.UI.Tamagotchis.Personalities;

public class NeutralPersonality : IPersonality
{
    /// <summary>
    /// Provides the images 
    /// </summary>
    /// <param name="mood"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
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