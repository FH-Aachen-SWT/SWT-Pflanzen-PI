namespace PflanzenPi.Sensor;

public interface ISensor<out TData>
{
    public event SensorDataChangedEvent<TData>? OnDatenChanged;
    
    public TData? Current { get; }
}