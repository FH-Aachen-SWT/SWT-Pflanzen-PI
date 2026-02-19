using PflanzenPi.Plants;
using PflanzenPi.Plants.Types;

namespace PflanzenPI.Persistence.Repository;

/// <summary>
/// Repository for Tamagotchi
/// </summary>
public interface ITamagotchiRepository
{
    /// <summary>
    /// Get the current plantType for the tamagotchi
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<PlantType> GetPlantType(string name);
    
    /// <summary>
    /// Update the current plantType of the current tamagotchi
    /// </summary>
    /// <param name="plantType"></param>
    /// <returns></returns>
    Task UpdatePlantType(PlantType plantType); 
    
    /// <summary>
    /// Update the brightnessType of the current tamagotchi
    /// </summary>
    /// <param name="brightnessType"></param>
    /// <returns></returns>
    Task UpdateBrightnessType(BrightnessType brightnessType);

    /// <summary>
    /// Update the personalityType of the current tamagotchi
    /// </summary>
    /// <param name="personalityType">new personality</param>
    /// <returns></returns>
    Task UpdatePersonalityType(PersonalityType personalityType);
    
    /// <summary>
    /// Update the name of the current tamagotchi 
    /// </summary>
    /// <param name="newName"></param>
    /// <returns></returns>
    Task UpdateName(string newName);
    
    /// <summary>
    /// Get the current BrightnessType for the tamagotchi
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<BrightnessType> GetBrightnessType(string name);
    
    /// <summary>
    /// Gets the currently selected tamagotchi
    /// </summary>
    /// <returns></returns>
    Task<string> GetCurrentTamagotchiName();
}