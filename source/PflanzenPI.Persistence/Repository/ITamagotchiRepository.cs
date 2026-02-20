using PflanzenPI.Persistence.Schema.Model;

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
    Task<PlantTypeDB> GetPlantTypeAsync(string name);
    
    /// <summary>
    /// Update the current plantType of the current tamagotchi
    /// </summary>
    /// <param name="plantType"></param>
    /// <returns></returns>
    Task UpdatePlantTypeAsync(PlantTypeDB plantType); 
    
    /// <summary>
    /// Update the brightnessType of the current tamagotchi
    /// </summary>
    /// <param name="brightnessType"></param>
    /// <returns></returns>
    Task UpdateBrightnessTypeAsync(BrightnessTypeDB brightnessType);
    
    /// <summary>
    /// Update the personalityType of the current tamagotchi
    /// </summary>
    /// <param name="personalityType">new personality</param>
    /// <returns></returns>
    Task UpdatePersonalityType(PersonalityTypeDB personalityType);
    
    /// <summary>
    /// Update the name of the current tamagotchi 
    /// </summary>
    /// <param name="newName"></param>
    /// <returns></returns>
    Task UpdateNameAsync(string newName);
    
    /// <summary>
    /// Get the current BrightnessType for the tamagotchi
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<BrightnessTypeDB> GetBrightnessTypeAsync(string name);
    
    /// <summary>
    /// Gets the currently selected tamagotchi
    /// </summary>
    /// <returns></returns>
    Task<string?> GetCurrentTamagotchiNameAsync();
}