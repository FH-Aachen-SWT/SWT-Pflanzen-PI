using System;
using PflanzenPi.UI.Tamagotchis.Moods;
using static PflanzenPi.UI.Tamagotchis.Moods.Mood;

namespace PflanzenPi.UI.Tamagotchis.Personalities;

public class HappyPersonality : IPersonality
{
    public string ProvideImage(Mood mood)
    {
        return mood switch
        {
            Happy => "satisfied.gif",
            Neutral => "satisfied.gif",
            Sad =>  "wetNeutral.gif",
            Angry => "drySad.gif",
            _ => throw new ArgumentOutOfRangeException(nameof(mood), mood, null)
        };
    }
}