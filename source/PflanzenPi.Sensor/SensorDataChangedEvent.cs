namespace PflanzenPi.Sensor;

public delegate void SensorDataChangedEvent<in TData>(TData? previousValue, TData newValue);