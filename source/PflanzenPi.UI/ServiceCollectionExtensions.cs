using System;
using Microsoft.Extensions.DependencyInjection;
using PflanzenPI.Persistence.Business;
using PflanzenPI.Persistence.Repository;
using PflanzenPi.Plants;
using PflanzenPi.Plants.Behaviours.BrightnessBehaviours;
using PflanzenPi.Plants.Behaviours.MoistureBehaviours;
using PflanzenPi.Plants.DBAdapter;
using PflanzenPi.Plants.PredictionModel;
using PflanzenPi.Sensor.Sensors;
using PflanzenPi.Sensor.Sensors.Mocks;
using PflanzenPi.UI.Tamagotchis;
using PflanzenPi.UI.Tamagotchis.Moods;
using PflanzenPi.UI.Tamagotchis.Personalities;
using PflanzenPi.UI.Tamagotchis.States;
using PflanzenPi.UI.Viewmodel;

namespace PflanzenPi.UI;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        //Streaks
        collection.AddSingleton<IStreakRepository, StreakRepository>();
        collection.AddSingleton<IStreakService, StreakService>();
        collection.AddSingleton<IStreakBatch, StreakBatch>();
        
        // Brightness
        IBrightnessRepository brightnessRepo = new BrightnessRepository();
        collection.AddSingleton<IBrightnessRepository>(brightnessRepo);
        IBrightnessService service = new BrightnessService(brightnessRepo);
        collection.AddSingleton<IBrightnessService>(service);
        
        //Repository
        collection.AddSingleton<ITamagotchiRepository, TamagotchiRepository>();
        
        // Mapping to and from DB
        collection.AddSingleton<IEnumMapper, EnumMapper>();
        
        // Sensor
        TimeSpan pollingRate = TimeSpan.FromMilliseconds(10000);
        ISensor<Moisture> moistureSensor = new MockMoistureSensorSlowDecline(pollingRate);
        ISensor<Brightness> brightnessSensor = new MockBrightnessSensorSine(30000, pollingRate, service);
        // ISensor<Moisture> sensor = new MoistureSensor(pollingRate); //REAL SENSOR
        // ISensor<Brightness> brightnessSensor = new BrightnessSensor(pollingRate, service); //REAL SENSOR
        collection.AddSingleton<ISensor<Moisture>>(moistureSensor);
        collection.AddSingleton<ISensor<Brightness>>(brightnessSensor);
        
        // Sensor-Service
        SensorService sensorService = new SensorService();
        sensorService.Register(moistureSensor);
        sensorService.Register(brightnessSensor);
        collection.AddSingleton<SensorService>(sensorService);
        collection.AddSingleton<ISensor<Brightness>>(brightnessSensor);
        
        // Prediction Service
        TimeSpan secondsUntilFirstPrediction = TimeSpan.FromSeconds(10);
        int samplesUntilFirstPrediction =
            (int)(secondsUntilFirstPrediction.TotalSeconds / pollingRate.TotalSeconds);
        int predictEveryXSamples = 2;
        IPredictionService predictionService = new PredictionService(pollingRate, samplesUntilFirstPrediction, predictEveryXSamples);
        collection.AddSingleton<IPredictionService>(predictionService);
        
        // Behaviours, Mood und Personality
        collection.AddSingleton<IMoistureBehaviourFactory, MoistureBehaviourFactory>();
        collection.AddSingleton<IBrightnessBehaviourFactory, BrightnessBehaviourFactory>();
        collection.AddSingleton<IPersonalityFactory, PersonalityFactory>();
        collection.AddSingleton<IMoistureImagesProvider, MoistureImagesProvider>();
        collection.AddSingleton<IBrightnessImagesProvider, BrightnessImagesProvider>();
        collection.AddSingleton<IMoodInterpreter, WeightedMoodInterpreter>();
        collection.AddSingleton<IPersonality, NeutralPersonality>();
        
        collection.AddSingleton<Plant>();
        collection.AddSingleton<Tamagotchi>();
        collection.AddSingleton<MainViewModel>();
    }
}