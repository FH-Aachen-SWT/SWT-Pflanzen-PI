namespace sensor;

public delegate void SensorDataChangedEvent<in TData>(TData? previousValue, TData newValue);