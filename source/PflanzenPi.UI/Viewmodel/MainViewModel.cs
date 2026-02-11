using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using PflanzenPi.UI.Tamagotchis;

namespace PflanzenPi.UI.Viewmodel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private Image currentImage;
    
    [ObservableProperty]
    private Tamagotchi tamagotchi;

    public MainViewModel(Tamagotchi tamagotchi)
    {
        Tamagotchi = tamagotchi;
    }
}