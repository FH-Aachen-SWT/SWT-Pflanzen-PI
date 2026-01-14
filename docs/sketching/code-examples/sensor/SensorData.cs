namespace sensor;

public abstract class SensorData<T>
{
    public T Value { get; }

    protected SensorData(T value)
    {
        Value = value;
    }
    override public string ToString() => Value?.ToString() ?? "";
}