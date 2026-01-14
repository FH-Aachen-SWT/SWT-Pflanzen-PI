namespace sensor;

public abstract class Sensor<TData> : ISensor<TData>
{
    public event Action<TData?, TData>? OnDatenChanged;
    public TData? Current { get; private set; }

    public void Publish(TData data)
    {
        var previous = Current;
        Current = data;
        OnDatenChanged?.Invoke(previous, data);
    }
}