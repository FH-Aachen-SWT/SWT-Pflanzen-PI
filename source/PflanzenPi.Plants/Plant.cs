using PflanzenPi.Sensor;

namespace PflanzenPi.Plants;

/// <summary>
/// Controller for calling interpretation of moisture levels when they have changed and passing changed moisture status to UI
/// </summary>
public class Plant
{
    private readonly SensorService _sensorService;
    
    public MoistureStatusChangedEvent? OnMoistureStatusChanged;
    
    public readonly IMoistureBehaviour _moistureBehaviour;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="sensorService">Sensor service</param>
    /// <param name="moistureBehaviour">Moisture behaviour of plant</param>
    /// <exception cref="ArgumentNullException">ArgumentNullException if moisture sensor is null</exception>
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
    

    /// <summary>
    /// Calls for interpretation of moisture status when moisture has changed. Emits moisture status changed event when moisture status has changed.
    /// </summary>
    /// <param name="prevMoisture">Previous moisture</param>
    /// <param name="nextMoisture">Next / Current moisture</param>
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