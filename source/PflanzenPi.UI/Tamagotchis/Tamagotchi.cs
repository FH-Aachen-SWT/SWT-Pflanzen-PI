using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using PflanzenPi.Plants;
using PflanzenPi.UI.Tamagotchis.Moods;
using PflanzenPi.UI.Tamagotchis.Personalities;
using PflanzenPi.UI.Tamagotchis.States;

namespace PflanzenPi.UI.Tamagotchis;

/// <summary>
/// Observable model for the Tamagotchi
/// </summary>
public partial class Tamagotchi : ObservableObject
{
    [ObservableProperty] private MoistureStatus currentMoistureStatus;
    
    [ObservableProperty] private ObservableCollection<Bitmap> currentMoistureImages = new();
    
    [ObservableProperty] private Bitmap? currentMoodImage;
    
    private readonly IMoodInterpreter _moodInterpreter;
    private readonly IPersonality _personality;
    private readonly IMoistureImagesProvider _moistureImagesProvider;

    private readonly Plant _plant;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="plant">Plant-model</param>
    /// <param name="moodInterpreter">Mood interpreter</param>
    /// <param name="personality">personality</param>
    public Tamagotchi(Plant plant, IMoodInterpreter moodInterpreter, IPersonality personality, IMoistureImagesProvider moistureImagesProvider)
    {
        _plant = plant;
        _moodInterpreter = moodInterpreter;
        _personality = personality;
        _moistureImagesProvider = moistureImagesProvider;
        OnMoistureStatusChanged(MoistureStatus.Satisfied);
        //_plant.OnMoistureStatusChanged += OnMoistureStatusChanged;
    }

    /// <summary>
    /// Calls for interpretation of new moisture statuses
    /// </summary>
    /// <param name="status">new moisture status</param>
    private void OnMoistureStatusChanged(MoistureStatus status)
    {
        CurrentMoistureStatus = status;
        var currentMood = _moodInterpreter.Interpret(status);
        Console.WriteLine($"Updated Status:  {status}");
        var moodImageName = _personality.ProvideImage(currentMood);
        if (!string.IsNullOrEmpty(moodImageName))
        {
            var uri = new Uri($"avares://PflanzenPi.UI/Assets/{moodImageName}");
            CurrentMoodImage = new Bitmap(AssetLoader.Open(uri));
            Console.WriteLine($"Loaded Image: {moodImageName}");
        }
        else
        {
            CurrentMoodImage = null;
        }
        var moistureImages = _moistureImagesProvider.ProvideImages(status);
        CurrentMoistureImages.Clear();
        foreach (var image in moistureImages)
        {
            var uri = new Uri($"avares://PflanzenPi.UI/Assets/{image}");
            CurrentMoistureImages.Add(new Bitmap(AssetLoader.Open(uri)));
            OnPropertyChanged(nameof(CurrentMoistureImages));
        }

    }
}