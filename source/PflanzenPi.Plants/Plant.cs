using PflanzenPi.Plants.Behaviours;
using PflanzenPi.Plants.Behaviours.BrightnessBehaviours;
using PflanzenPi.Plants.Behaviours.MoistureBehaviours;
using PflanzenPi.Sensor;

namespace PflanzenPi.Plants;

/// <summary>
/// Controller for calling interpretation of moisture levels when they have changed and passing changed moisture status to UI
/// </summary>
public class Plant
{
    public MoistureStatusChangedEvent? OnMoistureStatusChanged;
    public BrightnessStatusChangedEvent? OnBrightnessStatusChanged;
    private IMoistureBehaviour currentMoistureBehaviour;
    private IBrightnessBehaviour currentBrightnessBehaviour;
    private readonly IMoistureBehaviourFactory _moistureBehaviourFactory;
    private readonly IBrightnessBehaviourFactory _brightnessBehaviourFactory;
    private readonly ISensor<Moisture>  _moistureSensor;
    private readonly ISensor<Brightness>  _brightnessSensor;
    private readonly SensorService _sensorService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="sensorService">Sensor service</param>
    /// <param name="moistureBehaviourFactory">Moisture behaviour factory to create a behaviour for the PlantType</param>
    /// <exception cref="ArgumentNullException">ArgumentNullException if moisture sensor is null</exception>
    public Plant(SensorService sensorService, IMoistureBehaviourFactory moistureBehaviourFactory, IBrightnessBehaviourFactory brightnessBehaviourFactory)
    {
        _sensorService = sensorService;
        _moistureBehaviourFactory = moistureBehaviourFactory;
        _brightnessBehaviourFactory = brightnessBehaviourFactory;
        currentMoistureBehaviour = moistureBehaviourFactory.Create(PlantType.MediumWater);
        currentBrightnessBehaviour =  brightnessBehaviourFactory.Create(BrightnessType.MediumLight);
        ISensor<Moisture>? moistureSensor = _sensorService.Get<Moisture>();
        _moistureSensor = moistureSensor ?? throw new ArgumentNullException(nameof(moistureSensor));
        moistureSensor.OnDataChanged += UpdateMoisture;
        ISensor<Brightness>? brightnessSensor = _sensorService.Get<Brightness>();
        _brightnessSensor = brightnessSensor ?? throw new ArgumentNullException(nameof(brightnessSensor));
        brightnessSensor.OnDataChanged += UpdateBrightness;
    }

    /// <summary>
    /// Change the moisture behaviour of the Plant with a certain PlantType
    /// </summary>
    /// <param name="plantType"></param>
    public void ChangeMoistureBehaviour(PlantType plantType)
    {
        currentMoistureBehaviour = _moistureBehaviourFactory.Create(plantType);
        if (_moistureSensor.Current is not null)
        {
            //Force Update when switching behaviour
            UpdateMoisture(null, _moistureSensor.Current);
        }
        Console.WriteLine("MOISTURE BEHAVIOUR CHANGED: " + nameof(currentMoistureBehaviour));
    }
    
    /// <summary>
    /// Change the brightness behaviour of the Plant with a certain brightnessType
    /// </summary>
    /// <param name="brightnessType"></param>
    public void ChangeBrightnessBehaviour(BrightnessType brightnessType)
    {
        currentBrightnessBehaviour = _brightnessBehaviourFactory.Create(brightnessType);
        if (_brightnessSensor.Current is not null)
        {
            //Force Update when switching behaviour
            UpdateBrightness(null, _brightnessSensor.Current);
        }
        Console.WriteLine("BRIGHTNESS BEHAVIOUR CHANGED: " + nameof(currentBrightnessBehaviour));
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

    public void UpdateBrightness(Brightness? prevBrightness, Brightness nextBrightness)
    {
        BrightnessStatus? prevStatus = null;
        BrightnessStatus nextStatus;
        if (prevBrightness is not null && prevBrightness != nextBrightness)
        {
            prevStatus = currentBrightnessBehaviour.Interpret(prevBrightness);
        }
        nextStatus = currentBrightnessBehaviour.Interpret(nextBrightness);
        if (prevStatus is null || (prevBrightness != null && prevBrightness == 0) || prevStatus != nextStatus)
        {
            OnBrightnessStatusChanged?.Invoke(nextStatus);
        }
    }
}