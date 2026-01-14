namespace sensor;

public abstract class Sensor<TData> : ISensor<TData>
{
    public event SensorDataChangedEvent<TData>? OnDatenChanged;
    public TData? Current { get; protected set; }

    public void Publish(TData data)
    {
        var previous = Current;
        Current = data;
        OnDatenChanged?.Invoke(previous, data);
    }
}