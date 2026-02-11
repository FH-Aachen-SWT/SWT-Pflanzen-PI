namespace PflanzenPi.Sensor;

public interface ISensor<out TData>
{
    public event SensorDataChangedEvent<TData>? OnDataChanged;
    
    public TData? Current { get; }
}