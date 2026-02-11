using PflanzenPi.UI.Tamagotchis.Moods;

namespace PflanzenPi.UI.Tamagotchis.Personalities;

/// <summary>
/// Interface for personality
/// </summary>
public interface IPersonality
{
    /// <summary>
    /// Provides image based off of personality and mood
    /// </summary>
    /// <param name="mood">mood</param>
    /// <returns>image name</returns>
    string ProvideImage(Mood mood);
}