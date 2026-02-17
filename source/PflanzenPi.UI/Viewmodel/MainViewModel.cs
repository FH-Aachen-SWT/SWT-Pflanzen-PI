using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using PflanzenPi.Plants;
using PflanzenPi.UI.Tamagotchis;

namespace PflanzenPi.UI.Viewmodel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private Tamagotchi tamagotchi;
    
    [ObservableProperty]
    private PlantType[] allPlantTypes = Enum.GetValues<PlantType>();
    
    [ObservableProperty]
    private BrightnessType[] allBrightnessTypes = Enum.GetValues<BrightnessType>();

    public MainViewModel(Tamagotchi tamagotchi)
    {
        Tamagotchi = tamagotchi;
    }
}