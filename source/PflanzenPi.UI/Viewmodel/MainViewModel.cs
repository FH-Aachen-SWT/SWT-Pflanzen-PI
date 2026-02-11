using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using PflanzenPi.Plant;

namespace PflanzenPi.UI.Viewmodel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private Image currentImage;
    
    [ObservableProperty]
    private Tamagotchi tamagotchi;

    public MainViewModel()
    {
        tamagotchi  = new Tamagotchi();
        Test();
    }

    public void Test()
    {
        
    }
    
    
}