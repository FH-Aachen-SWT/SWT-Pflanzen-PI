using System;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
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
    [ObservableProperty] private string name;
    
    [ObservableProperty] private PlantType currentPlantType;
    
    [ObservableProperty] private BrightnessType currentBrightnessType;
    
    [ObservableProperty] private MoistureStatus currentMoistureStatus;
    
    [ObservableProperty] private BrightnessStatus currentBrightnessStatus;
    
    [ObservableProperty] private ObservableCollection<Bitmap> currentMoistureImages = new();
    
    [ObservableProperty] private ObservableCollection<Bitmap> currentBrightnessImages = new();
    
    [ObservableProperty] private string? currentMoodImage;
    
    private readonly IMoodInterpreter _moodInterpreter;
    private readonly IPersonality _personality;
    private readonly IMoistureImagesProvider _moistureImagesProvider;
    private readonly Plant _plant;
    private readonly IBrightnessImagesProvider _brightnessImagesProvider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="plant">Plant-model</param>
    /// <param name="moodInterpreter">Mood interpreter</param>
    /// <param name="personality">personality</param>
    /// <param name="moistureImagesProvider">moistureImagesProvider</param>
    public Tamagotchi(Plant plant, IMoodInterpreter moodInterpreter, IPersonality personality, IMoistureImagesProvider moistureImagesProvider, IBrightnessImagesProvider brightnessImagesProvider)
    {
        Name = "Bob.B";
        _plant = plant;
        _moodInterpreter = moodInterpreter;
        _personality = personality;
        _moistureImagesProvider = moistureImagesProvider;
        _brightnessImagesProvider  = brightnessImagesProvider;
        CurrentPlantType = PlantType.MediumWater;
        OnMoistureStatusChanged(MoistureStatus.Satisfied);
        OnBrightnessStatusChanged(BrightnessStatus.Satisfied);
        _plant.OnMoistureStatusChanged += OnMoistureStatusChanged;
        _plant.OnBrightnessStatusChanged += OnBrightnessStatusChanged;
    }

    /// <summary>
    /// Change moisture behaviour when plantType changes in the UI
    /// </summary>
    /// <param name="plantType"></param>
    partial void OnCurrentPlantTypeChanged(PlantType plantType)
    {
        _plant.ChangeMoistureBehaviour(plantType);
    }

    
    /// <summary>
    /// Change brightness behaviour when brightnessType changes in the UI
    /// </summary>
    /// <param name="brightnessType"></param>
    partial void OnCurrentBrightnessTypeChanged(BrightnessType brightnessType)
    {
        _plant.ChangeBrightnessBehaviour(brightnessType);
    }

    /// <summary>
    /// Calls for interpretation of new brightness statuses
    /// </summary>
    /// <param name="brightnessStatus"></param>
    private void OnBrightnessStatusChanged(BrightnessStatus brightnessStatus)
    {
        Dispatcher.UIThread.Post(() =>
        {
            var brightnessImages = _brightnessImagesProvider.ProvideImages(brightnessStatus);
            CurrentBrightnessImages.Clear();
            foreach (var image in brightnessImages)
            {
                var uri = new Uri($"{AssetConstants.ASSET_BASE_PATH}{image}");
                CurrentBrightnessImages.Add(new Bitmap(AssetLoader.Open(uri)));
            }
        });
    }

    /// <summary>
    /// Calls for interpretation of new moisture statuses
    /// </summary>
    /// <param name="status">new moisture status</param>
    private void OnMoistureStatusChanged(MoistureStatus status)
    {
        CurrentMoistureStatus = status;
        var currentMood = _moodInterpreter.Interpret(status, CurrentBrightnessStatus);
        Console.WriteLine($"Updated Status:  {status}");
        var moodImageName = _personality.ProvideImage(currentMood);
        Dispatcher.UIThread.Post(() =>
        {
            if (!string.IsNullOrEmpty(moodImageName))
            {
                CurrentMoodImage = $"{AssetConstants.ASSET_BASE_PATH}{moodImageName}";
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
                var uri = new Uri($"{AssetConstants.ASSET_BASE_PATH}{image}");
                CurrentMoistureImages.Add(new Bitmap(AssetLoader.Open(uri)));
            }
        });
    }
}