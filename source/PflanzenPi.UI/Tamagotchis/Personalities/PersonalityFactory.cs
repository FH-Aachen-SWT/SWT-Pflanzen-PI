using System;
using PflanzenPi.Plants.Types;

namespace PflanzenPi.UI.Tamagotchis.Personalities;

public class PersonalityFactory : IPersonalityFactory
{
    public IPersonality Create(PersonalityType personalityType)
    {
        return personalityType switch
        {
            PersonalityType.Happy => new HappyPersonality(),
            PersonalityType.Neutral => new NeutralPersonality(),
            PersonalityType.Sad => new SadPersonality(),
            _ => throw new ArgumentOutOfRangeException(nameof(personalityType), personalityType, null)
        };
    }
}