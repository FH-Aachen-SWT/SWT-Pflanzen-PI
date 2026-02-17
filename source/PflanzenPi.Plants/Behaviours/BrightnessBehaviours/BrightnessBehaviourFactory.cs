namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

/// <summary>
/// BrightnessBehviourFactory Implementation
/// </summary>
public class BrightnessBehaviourFactory : IBrightnessBehaviourFactory
{
    public IBrightnessBehaviour Create(BrightnessType brightnessType)
    {
        return brightnessType switch
        {
            BrightnessType.LittleLight => new LittleLight(),
            BrightnessType.MediumLight => new MediumLight(),
            BrightnessType.MuchLight => new MuchLight(),
            _ => throw new ArgumentOutOfRangeException(nameof(brightnessType), brightnessType, null)
        };

    }
}