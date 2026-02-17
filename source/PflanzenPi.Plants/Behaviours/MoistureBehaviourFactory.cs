namespace PflanzenPi.Plants.Behaviours;

/// <summary>
/// Implementation of MoistureBehaviourFactory
/// </summary>
public class MoistureBehaviourFactory : IMoistureBehaviourFactory
{
    public IMoistureBehaviour Create(PlantType plantType)
    {
        return plantType switch
        {
            PlantType.LittleWater => new LittleMoisture(),
            PlantType.MediumWater => new MediumMoisture(),
            PlantType.MuchWater => new MuchMoisture(),
            _ => throw new ArgumentOutOfRangeException(nameof(plantType), plantType, null)
        };
    }
}