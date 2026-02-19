using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PflanzenPI.Persistence.Repository;
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
    [ObservableProperty] private string? name;
    
    [ObservableProperty] private PlantType currentPlantType;
    
    [ObservableProperty] private BrightnessType currentBrightnessType;
    
    [ObservableProperty] private MoistureStatus currentMoistureStatus;
    
    [ObservableProperty] private BrightnessStatus currentBrightnessStatus;
    
    [ObservableProperty] private ObservableCollection<Bitmap> currentMoistureImages = new();
    
    [ObservableProperty] private ObservableCollection<Bitmap> currentBrightnessImages = new();
    
    [ObservableProperty] private string? currentMoodImage;

    public IAsyncRelayCommand<PlantType> PlantTypeChangedCommand => new AsyncRelayCommand<PlantType>(OnPlantTypeChanged);
    public IAsyncRelayCommand<BrightnessType> BrightnessTypeChangedCommand => new AsyncRelayCommand<BrightnessType>(OnBrightnessTypeChanged);
    
    public IAsyncRelayCommand<string> NameChangedCommand => new AsyncRelayCommand<string>(OnTamagotchiNameChanged);
    
    private readonly IMoodInterpreter _moodInterpreter;
    private readonly IPersonality _personality;
    private readonly IMoistureImagesProvider _moistureImagesProvider;
    private readonly Plant _plant;
    private readonly IBrightnessImagesProvider _brightnessImagesProvider;
    private readonly ITamagotchiRepository _tamagotchiRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="plant">Plant-model</param>
    /// <param name="moodInterpreter">Mood interpreter</param>
    /// <param name="personality">personality</param>
    /// <param name="moistureImagesProvider">moistureImagesProvider</param>
    public Tamagotchi(Plant plant, IMoodInterpreter moodInterpreter, IPersonality personality,
        IMoistureImagesProvider moistureImagesProvider, IBrightnessImagesProvider brightnessImagesProvider, ITamagotchiRepository tamagotchiRepository)
    {
        _tamagotchiRepository = tamagotchiRepository;
        _plant = plant;
        Dispatcher.UIThread.InvokeAsync(async () =>
        {
            Name = await _tamagotchiRepository.GetCurrentTamagotchiName();
            CurrentPlantType = await _tamagotchiRepository.GetPlantType(Name);
            CurrentBrightnessType = await _tamagotchiRepository.GetBrightnessType(Name);
            _plant.ChangeMoistureBehaviour(CurrentPlantType);
            _plant.ChangeBrightnessBehaviour(CurrentBrightnessType);
        });
        _moodInterpreter = moodInterpreter;
        _personality = personality;
        _moistureImagesProvider = moistureImagesProvider;
        _brightnessImagesProvider  = brightnessImagesProvider;
        _plant.OnMoistureStatusChanged += OnMoistureStatusChanged;
        _plant.OnBrightnessStatusChanged += OnBrightnessStatusChanged;
    }

    /// <summary>
    /// Change moisture behaviour when plantType changes in the UI
    /// </summary>
    /// <param name="plantType"></param>
    private async Task OnPlantTypeChanged(PlantType plantType)
    {
        _plant.ChangeMoistureBehaviour(plantType);
        await _tamagotchiRepository.UpdatePlantType(plantType);
    }

    
    /// <summary>
    /// Change brightness behaviour when brightnessType changes in the UI
    /// </summary>
    /// <param name="brightnessType"></param>
    private async Task OnBrightnessTypeChanged(BrightnessType brightnessType)
    {
        _plant.ChangeBrightnessBehaviour(brightnessType);
        await _tamagotchiRepository.UpdateBrightnessType(brightnessType);
    }

    /// <summary>
    /// change the name of the tamagotchi when Name changes in the UI
    /// </summary>
    /// <param name="tamagotchiName"></param>
    private async Task OnTamagotchiNameChanged(string? tamagotchiName)
    {
        if (string.IsNullOrWhiteSpace(tamagotchiName))
        {
            return;
        }
        await _tamagotchiRepository.UpdateName(tamagotchiName);
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