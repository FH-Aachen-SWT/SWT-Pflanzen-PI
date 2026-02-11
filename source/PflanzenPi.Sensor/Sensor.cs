namespace PflanzenPi.Sensor;

public abstract class Sensor<TData> : ISensor<TData>
{
    public event SensorDataChangedEvent<TData>? OnDataChanged;
    public TData? Current { get; protected set; }

    protected void Publish(TData data)
    {
        /*
         Beispiel-Implementierung aus den Sketches
        var previous = Current;
        Current = data;
        OnDatenChanged?.Invoke(previous, data);
        */
    }
}