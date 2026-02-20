using System;
using PflanzenPi.UI.Tamagotchis.Moods;
using static PflanzenPi.UI.Tamagotchis.Moods.Mood;

namespace PflanzenPi.UI.Tamagotchis.Personalities;

public class SadPersonality : IPersonality
{
    public string ProvideImage(Mood mood)
    {
        return mood switch
        {
            Happy => "wetNeutral.gif",
            Neutral => "drySad.gif",
            Sad =>  "drySad.gif",
            Angry => "dryWetAngry.gif",
            _ => throw new ArgumentOutOfRangeException(nameof(mood), mood, null)
        };
    }
}