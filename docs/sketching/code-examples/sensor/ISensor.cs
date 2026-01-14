namespace sensor;

public interface ISensor<out TData>
{
    event Action<TData?, TData> OnDatenChanged;
    TData? Current { get; }
}