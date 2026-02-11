using Microsoft.Extensions.DependencyInjection;
using PflanzenPi.Sensor;
using PflanzenPi.UI.Viewmodel;

namespace PflanzenPi.UI;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddSingleton<ISensor<Moisture>, MoistureSensor>();
        collection.AddSingleton<MainViewModel>();
    }
}