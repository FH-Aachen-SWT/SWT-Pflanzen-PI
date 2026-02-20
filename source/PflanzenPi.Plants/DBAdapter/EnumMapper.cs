using PflanzenPI.Persistence.Schema.Model;
using PflanzenPi.Plants.Types;

namespace PflanzenPi.Plants.DBAdapter;

public class EnumMapper : IEnumMapper
{
    public PlantTypeDB map(PlantType plantType)
    {
        return plantType switch
        {
            PlantType.LittleWater => PlantTypeDB.LittleWater,
            PlantType.MediumWater => PlantTypeDB.MediumWater,
            PlantType.MuchWater => PlantTypeDB.MuchWater,
            _ => throw new NotSupportedException($"No equivalent to {plantType} in PlantTypeDB")
        };
    }

    public PlantType map(PlantTypeDB plantTypeDb)
    {
        return plantTypeDb switch
        {
            PlantTypeDB.LittleWater => PlantType.LittleWater,
            PlantTypeDB.MediumWater => PlantType.MediumWater,
            PlantTypeDB.MuchWater => PlantType.MuchWater,
            _ => throw new NotSupportedException($"No equivalent to {plantTypeDb} in PlantType")
        };
    }

    public BrightnessTypeDB map(BrightnessType brightnessType)
    {
        return brightnessType switch
        {
            BrightnessType.LittleLight => BrightnessTypeDB.LittleLight,
            BrightnessType.MediumLight => BrightnessTypeDB.MediumLight,
            BrightnessType.MuchLight => BrightnessTypeDB.MuchLight,
            _ => throw new NotSupportedException($"No equivalent to {brightnessType} in BrightnessTypeDB")
        };
    }

    public BrightnessType map(BrightnessTypeDB brightnessTypeDb)
    {
        return brightnessTypeDb switch
        {
            BrightnessTypeDB.LittleLight => BrightnessType.LittleLight,
            BrightnessTypeDB.MediumLight => BrightnessType.MediumLight,
            BrightnessTypeDB.MuchLight => BrightnessType.MuchLight,
            _ => throw new NotSupportedException($"No equivalent to {brightnessTypeDb} in BrightnessType")
        };
    }

    public PersonalityTypeDB map(PersonalityType personalityType)
    {
        return personalityType switch
        {
            PersonalityType.Neutral => PersonalityTypeDB.Neutral,
            PersonalityType.Happy => PersonalityTypeDB.Happy,
            PersonalityType.Sad => PersonalityTypeDB.Sad,
            _ => throw new NotSupportedException($"No equivalent to {personalityType} in PersonalityTypeDB")
        };
    }

    public PersonalityType map(PersonalityTypeDB personalityTypeDb)
    {
        return personalityTypeDb switch
        {
            PersonalityTypeDB.Neutral => PersonalityType.Neutral,
            PersonalityTypeDB.Happy => PersonalityType.Happy,
            PersonalityTypeDB.Sad => PersonalityType.Sad,
            _ => throw new NotSupportedException($"No equivalent to {personalityTypeDb} in PersonalityType")
        };
    }
}