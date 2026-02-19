using PflanzenPi.Plants.Types;

namespace PflanzenPi.Plants.Behaviours.MoistureBehaviours;

/// <summary>
/// Factory for moisture behaviours 
/// </summary>
public interface IMoistureBehaviourFactory
{
    /// <summary>
    /// Creates the behavior or null if PlantType is not mapped to a behaviour
    /// </summary>
    /// <param name="plantType"></param>
    /// <returns></returns>
    public IMoistureBehaviour Create(PlantType plantType);
}