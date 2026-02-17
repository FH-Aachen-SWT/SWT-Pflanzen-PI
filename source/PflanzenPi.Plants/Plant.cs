using PflanzenPi.Plants.Behaviours;
using PflanzenPi.Sensor;

namespace PflanzenPi.Plants;

/// <summary>
/// Controller for calling interpretation of moisture levels when they have changed and passing changed moisture status to UI
/// </summary>
public class Plant
{
    private readonly SensorService _sensorService;
    
    public MoistureStatusChangedEvent? OnMoistureStatusChanged;
    
    private readonly IMoistureBehaviourFactory _moistureBehaviourFactory;
    
    private readonly ISensor<Moisture>  _moistureSensor;
    
    private IMoistureBehaviour currentMoistureBehaviour;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="sensorService">Sensor service</param>
    /// <param name="moistureBehaviourFactory">Moisture behaviour factory to create a behaviour for the PlantType</param>
    /// <exception cref="ArgumentNullException">ArgumentNullException if moisture sensor is null</exception>
    public Plant(SensorService sensorService, IMoistureBehaviourFactory moistureBehaviourFactory)
    {
        _sensorService = sensorService;
        _moistureBehaviourFactory = moistureBehaviourFactory;
        currentMoistureBehaviour = moistureBehaviourFactory.Create(PlantType.MediumWater);
        ISensor<Moisture>? moistureSensor = _sensorService.Get<Moisture>();
        _moistureSensor = moistureSensor ?? throw new ArgumentNullException(nameof(moistureSensor));
        moistureSensor.OnDataChanged += UpdateMoisture;
    }

    /// <summary>
    /// Change the behaviour of the Plant with a certain PlantType
    /// </summary>
    /// <param name="plantType"></param>
    public void ChangeBehaviour(PlantType plantType)
    {
        currentMoistureBehaviour = _moistureBehaviourFactory.Create(plantType);
        if (_moistureSensor.Current is not null)
        {
            //Force Update when switching behaviour
            UpdateMoisture(null, _moistureSensor.Current);
        }
        Console.WriteLine("BEHAVIOUR CHANGED: " + nameof(currentMoistureBehaviour));
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
            prevStatus = currentMoistureBehaviour.Interpret(prevMoisture);
        }
        nextStatus = currentMoistureBehaviour.Interpret(nextMoisture);
        if (prevStatus is null || (prevMoisture != null && prevMoisture == 0) || prevStatus != nextStatus)
        {
            OnMoistureStatusChanged?.Invoke(nextStatus);
        }
    }
}