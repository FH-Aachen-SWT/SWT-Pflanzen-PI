namespace PflanzenPi.Sensor;

/// <summary>
/// Interface for sensor
/// </summary>
/// <typeparam name="TData">Datatype</typeparam>
public interface ISensor<out TData>
{
    public event SensorDataChangedEvent<TData>? OnDataChanged;
    
    public TData? Current { get; }
}