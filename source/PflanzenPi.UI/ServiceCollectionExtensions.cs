using Microsoft.Extensions.DependencyInjection;
using PflanzenPi.UI.Viewmodel;

namespace PflanzenPi.UI;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        //collection.AddSingleton<IRepository, Repository>();
        collection.AddTransient<MainViewModel>();
    }
}