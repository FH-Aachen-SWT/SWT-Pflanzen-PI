using PflanzenPi.Sensor;

namespace PflanzenPi.Plants;

/// <summary>
/// Interface for interpreting moisture levels to a moisture status depending on plant-type
/// </summary>
public interface IMoistureBehaviour
{
    /// <summary>
    /// Interprets the moisture level to a moisture status depending on plant-type
    /// </summary>
    /// <param name="moisture">Moisture</param>
    /// <returns>Moisture status depending on plant-type</returns>
    MoistureStatus Interpret(Moisture moisture);
}