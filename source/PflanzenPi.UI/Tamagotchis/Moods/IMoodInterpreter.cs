using PflanzenPi.Plants;

namespace PflanzenPi.UI.Tamagotchis.Moods;

/// <summary>
/// Interface for interpreting mood based off of statuses
/// </summary>
public interface IMoodInterpreter
{
    /// <summary>
    /// Interprets mood
    /// </summary>
    /// <param name="moisture">Moisture status</param>
    /// <returns>mood</returns>
    Mood Interpret(MoistureStatus moisture);
}