namespace PflanzenPi.Sensor;

public abstract class Sensor<TData> : ISensor<TData>
{
    public event SensorDataChangedEvent<TData>? OnDataChanged;
    public TData? Current { get; protected set; }

    protected void Publish(TData data)
    {
        var previous = Current;
        Current = data;
        OnDataChanged?.Invoke(previous, data);
    }
}