using PflanzenPi.Sensor;

namespace PflanzenPi.Plants;

public interface IMoistureBehaviour
{
    MoistureStatus Interpret(Moisture moisture);
}