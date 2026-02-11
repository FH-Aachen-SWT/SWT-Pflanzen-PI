namespace sensor;

public interface ISensor<out TData>
{
    event SensorDataChangedEvent<TData> OnDataChanged;
    TData? Current { get; }
}