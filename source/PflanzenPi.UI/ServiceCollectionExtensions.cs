using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PflanzenPi.Plants;
using PflanzenPi.Plants.Behaviours;
using PflanzenPi.Sensor;
using PflanzenPi.Sensor.Mocks;
using PflanzenPi.UI.Tamagotchis;
using PflanzenPi.UI.Tamagotchis.Moods;
using PflanzenPi.UI.Tamagotchis.Personalities;
using PflanzenPi.UI.Viewmodel;

namespace PflanzenPi.UI;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        // Sensor
        ISensor<Moisture> sensor = new MockMoistureSensor();
        collection.AddSingleton<ISensor<Moisture>>(sensor);
        // Sensor-Service
        SensorService sensorService = new SensorService();
        sensorService.Register(sensor);
        collection.AddSingleton<SensorService>(sensorService);
        // Moisture Behaviour, Mood und Personality
        collection.AddSingleton<IMoistureBehaviour, MediumMoisture>();
        collection.AddSingleton<IMoodInterpreter, DefaultMoodInterpreter>();
        collection.AddSingleton<IPersonality, NeutralPersonality>();
        
        collection.AddSingleton<Plant>();
        collection.AddSingleton<Tamagotchi>();
        collection.AddSingleton<MainViewModel>();
    }
}