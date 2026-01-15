using CommunityToolkit.Mvvm.ComponentModel;
using PflanzenPi.Plant;

namespace PflanzenPi.UI;

public partial class Tamagotchi : ObservableObject
{
    [ObservableProperty] private MoistureStatus currentMoistureStatus;
    
    [ObservableProperty] private string currentImageString;

    public Tamagotchi()
    {
        CurrentMoistureStatus = MoistureStatus.NichtTest;
    }
}