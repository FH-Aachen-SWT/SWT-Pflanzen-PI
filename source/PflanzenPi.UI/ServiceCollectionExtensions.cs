using System;
using Microsoft.Extensions.DependencyInjection;
using PflanzenPI.Persistence.Repository;
using PflanzenPi.Plants;
using PflanzenPi.Plants.Behaviours.BrightnessBehaviours;
using PflanzenPi.Plants.Behaviours.MoistureBehaviours;
using PflanzenPi.Sensor;
using PflanzenPi.Sensor.Mocks;
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
        //Repository
        collection.AddSingleton<ITamagotchiRepository, TamagotchiRepository>();
        
        // Sensor
        TimeSpan pollingRate = TimeSpan.FromMilliseconds(1000);
        ISensor<Moisture> moistureSensor = new MockMoistureSensorSine(40, pollingRate);
        ISensor<Brightness> brightnessSensor = new MockBrightnessSensorSine(30000, pollingRate);
        // ISensor<Moisture> sensor = new MoistureSensor(pollingRate); //REAL SENSOR
        // ISensor<Brightness> brightnessSensor = new MockBrightnessSensorSine(40, pollingRate); //REAL SENSOR
        collection.AddSingleton<ISensor<Moisture>>(moistureSensor);
        collection.AddSingleton<ISensor<Brightness>>(brightnessSensor);
        // Sensor-Service
        SensorService sensorService = new SensorService();
        sensorService.Register(moistureSensor);
        sensorService.Register(brightnessSensor);
        collection.AddSingleton<SensorService>(sensorService);
        collection.AddSingleton<ISensor<Brightness>>(brightnessSensor);
        // Behaviours, Mood und Personality
        collection.AddSingleton<IMoistureBehaviourFactory, MoistureBehaviourFactory>();
        collection.AddSingleton<IBrightnessBehaviourFactory, BrightnessBehaviourFactory>();
        collection.AddSingleton<IMoistureImagesProvider, MoistureImagesProvider>();
        collection.AddSingleton<IBrightnessImagesProvider, BrightnessImagesProvider>();
        collection.AddSingleton<IMoodInterpreter, WeightedMoodInterpreter>();
        collection.AddSingleton<IPersonality, NeutralPersonality>();
        
        collection.AddSingleton<Plant>();
        collection.AddSingleton<Tamagotchi>();
        collection.AddSingleton<MainViewModel>();
    }
}