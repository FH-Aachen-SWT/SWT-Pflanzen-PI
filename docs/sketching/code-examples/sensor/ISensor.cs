namespace sensor;

public interface ISensor<out TData>
{
    event SensorDataChangedEvent<TData> OnDatenChanged;
    TData? Current { get; }
}