namespace PflanzenPi.Sensor.Sensors;

/// <summary>
/// Sensor data changed event
/// </summary>
/// <typeparam name="TData">Previous and new value</typeparam>
public delegate void SensorDataChangedEvent<in TData>(TData? previousValue, TData newValue);