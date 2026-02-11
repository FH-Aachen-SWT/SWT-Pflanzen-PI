namespace PflanzenPi.Sensor;

/// <summary>
/// Abstract class to publish changed data from sensors with given datatype
/// </summary>
/// <typeparam name="TData"></typeparam>
public abstract class Sensor<TData> : ISensor<TData>
{
    public event SensorDataChangedEvent<TData>? OnDataChanged;
    public TData? Current { get; protected set; }

    /// <summary>
    /// Invokes on data changed event with old and new data
    /// </summary>
    /// <param name="data">Data of datatype</param>
    protected void Publish(TData data)
    {
        var previous = Current;
        Current = data;
        OnDataChanged?.Invoke(previous, data);
    }
}