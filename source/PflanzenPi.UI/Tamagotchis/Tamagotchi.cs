using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using PflanzenPi.Plants;
using PflanzenPi.UI.Tamagotchis.Moods;
using PflanzenPi.UI.Tamagotchis.Personalities;

namespace PflanzenPi.UI.Tamagotchis;

public partial class Tamagotchi : ObservableObject
{
    [ObservableProperty] private MoistureStatus currentMoistureStatus;
    
    [ObservableProperty] private Bitmap? currentMoodImage;
    
    private readonly IMoodInterpreter _moodInterpreter;
    private readonly IPersonality _personality;

    private readonly Plant _plant;

    public Tamagotchi(Plant plant, IMoodInterpreter moodInterpreter, IPersonality personality)
    {
        _plant = plant;
        _moodInterpreter = moodInterpreter;
        _personality = personality;
        _plant.OnMoistureStatusChanged += OnMoistureStatusChanged;
    }

    private void OnMoistureStatusChanged(MoistureStatus status)
    {
        CurrentMoistureStatus = status;
        var currentMood = _moodInterpreter.Interpret(status);
        Console.WriteLine($"Updated Status:  {status}");
        var moodImageName = _personality.ProvideImage(currentMood);
        if (string.IsNullOrEmpty(moodImageName))
        {
            CurrentMoodImage = null;
            return;
        }
        var uri = new Uri($"avares://PflanzenPi.UI/Assets/{moodImageName}");
        CurrentMoodImage = new Bitmap(AssetLoader.Open(uri));
        Console.WriteLine($"Loaded Image: {moodImageName}");
 
    }
}