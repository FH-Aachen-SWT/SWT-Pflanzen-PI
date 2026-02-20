using PflanzenPI.Persistence.Schema.Model;
using PflanzenPi.Plants.Types;

namespace PflanzenPi.Plants.DBAdapter;

public interface IEnumMapper
{
    PlantTypeDB map(PlantType plantType);

    PlantType map(PlantTypeDB plantTypeDb);

    BrightnessTypeDB map(BrightnessType brightnessType);

    BrightnessType map(BrightnessTypeDB brightnessTypeDb);

    PersonalityTypeDB map(PersonalityType personalityType);

    PersonalityType map(PersonalityTypeDB personalityTypeDb);
}