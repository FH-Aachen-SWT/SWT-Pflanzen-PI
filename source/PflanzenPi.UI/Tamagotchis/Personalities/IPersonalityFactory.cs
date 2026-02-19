using PflanzenPi.Plants.Types;

namespace PflanzenPi.UI.Tamagotchis.Personalities;

/// <summary>
/// Interface for factory for Personality
/// </summary>
public interface IPersonalityFactory
{
    /// <summary>
    /// Creates the IPersonality for the personalityType
    /// </summary>
    /// <param name="personalityType">PersonalityType</param>
    /// <returns></returns>
    IPersonality Create(PersonalityType personalityType);
}