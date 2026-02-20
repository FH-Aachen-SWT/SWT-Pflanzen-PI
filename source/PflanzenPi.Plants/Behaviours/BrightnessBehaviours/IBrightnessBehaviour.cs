using PflanzenPi.Sensor.Sensors;

namespace PflanzenPi.Plants.Behaviours.BrightnessBehaviours;

/// <summary>
/// Interface for interpreting brightness levels to a brightness status depending on plant-type
/// </summary>
public interface IBrightnessBehaviour
{
    /// <summary>
    /// Interprets the brightness level to a brightness status depending on plant-type
    /// </summary>
    /// <param name="brightness">Brightness</param>
    /// <returns>Brightness status depending on plant-type</returns>
    BrightnessStatus Interpret(Brightness brightness);
}