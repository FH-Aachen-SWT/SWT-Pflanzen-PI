using PflanzenPi.Plants.Types;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

/// <summary>
/// Brightnessfactory for IBrightnessBehaviour
/// </summary>
public interface IBrightnessBehaviourFactory
{
    /// <summary>
    /// Creates the IBrightnessBehaviour for the BrightnessType
    /// </summary>
    /// <param name="brightnessType"></param>
    /// <returns></returns>
    IBrightnessBehaviour Create(BrightnessType brightnessType);
}