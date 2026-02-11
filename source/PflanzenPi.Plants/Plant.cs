using PflanzenPi.Sensor;

namespace PflanzenPi.Plants;

public class Plant
{
    private readonly SensorService _sensorService;
    
    public MoistureStatusChangedEvent? OnMoistureStatusChanged;
    
    public readonly IMoistureBehaviour _moistureBehaviour;

    public Plant(SensorService sensorService, IMoistureBehaviour moistureBehaviour)
    {
        _sensorService = sensorService;
        _moistureBehaviour = moistureBehaviour;
        ISensor<Moisture>? moistureSensor = _sensorService.Get<Moisture>();
        if (moistureSensor is null)
        {
            throw new ArgumentNullException(nameof(moistureSensor));
        }
        moistureSensor.OnDataChanged += UpdateMoisture;
    }

    public void UpdateMoisture(Moisture? prevMoisture, Moisture nextMoisture)
    {
        MoistureStatus? prevStatus = null;
        MoistureStatus nextStatus;
        if (prevMoisture is not null && prevMoisture != nextMoisture)
        {
            prevStatus = _moistureBehaviour.Interpret(prevMoisture);
        }
        nextStatus = _moistureBehaviour.Interpret(nextMoisture);
        if (prevStatus is null || prevStatus != nextStatus)
        {
            OnMoistureStatusChanged?.Invoke(nextStatus);
        }
    }
}