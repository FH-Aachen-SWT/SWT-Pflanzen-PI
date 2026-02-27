using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PflanzenPI.Persistence.Business;
using PflanzenPI.Persistence.Business.Errors;
using PflanzenPI.Persistence.Database;
using PflanzenPI.Persistence.Repository;
using PflanzenPi.Plants;
using PflanzenPi.Plants.Types;
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

    [ObservableProperty] private PersonalityType currentPersonalityType;
    
    [ObservableProperty] private MoistureStatus currentMoistureStatus;
    
    [ObservableProperty] private BrightnessStatus currentBrightnessStatus;
    
    [ObservableProperty] private ObservableCollection<Bitmap> currentMoistureImages = new();
    
    [ObservableProperty] private ObservableCollection<Bitmap> currentBrightnessImages = new();
    
    [ObservableProperty] private string? currentMoodImage;
    
    [ObservableProperty] private int hoursUntilWateringPrediction;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(CurrentHeartImage))] private long? todaysStreak;
    
    /// <summary>
    /// Cached value if the streak is already reached
    /// </summary>
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(CurrentHeartImage))]  private bool streakReachedToday; 

    /// <summary>
    /// The heart to display, depending on TodaysStreak
    /// </summary>
    public Bitmap CurrentHeartImage => !StreakReachedToday ? GetBitmap("grayHeart.png") : GetBitmap("heart.png");

    public IAsyncRelayCommand<PlantType> PlantTypeChangedCommand => new AsyncRelayCommand<PlantType>(OnPlantTypeChanged);
    public IAsyncRelayCommand<BrightnessType> BrightnessTypeChangedCommand => new AsyncRelayCommand<BrightnessType>(OnBrightnessTypeChanged);

    public IAsyncRelayCommand<PersonalityType> PersonalityTypeChangedCommand =>
        new AsyncRelayCommand<PersonalityType>(OnPersonalityTypeChanged);
    public IAsyncRelayCommand<string> NameChangedCommand => new AsyncRelayCommand<string>(OnTamagotchiNameChanged);
    
    private readonly IMoodInterpreter _moodInterpreter;
    private readonly IPersonalityFactory _personalityFactory;
    private readonly IMoistureImagesProvider _moistureImagesProvider;
    private readonly Plant _plant;
    private readonly IBrightnessImagesProvider _brightnessImagesProvider;
    private readonly ITamagotchiRepository _tamagotchiRepository;
    private readonly Dictionary<string, Bitmap> _cachedBitmaps = [];
    
    private IPersonality _personality;
    private readonly IStreakService _streakService;
    private readonly IStreakBatch _streakBatch;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="plant">Plant-model</param>
    /// <param name="moodInterpreter">Mood interpreter</param>
    /// <param name="personality">personality</param>
    /// <param name="moistureImagesProvider">moistureImagesProvider</param>
    public Tamagotchi(Plant plant, IMoodInterpreter moodInterpreter, IPersonality personality,
        IMoistureImagesProvider moistureImagesProvider, IBrightnessImagesProvider brightnessImagesProvider, ITamagotchiRepository tamagotchiRepository, IStreakService streakService, IStreakBatch streakBatch,  IPersonalityFactory personalityFactory)
    {
        _streakBatch = streakBatch;
        _streakService = streakService;
        _tamagotchiRepository = tamagotchiRepository;
        _plant = plant;
        Dispatcher.UIThread.InvokeAsync(InitializeTamagotchi);
        _moodInterpreter = moodInterpreter;
        _personalityFactory = personalityFactory;
        _personality = personality;
        _moistureImagesProvider = moistureImagesProvider;
        _brightnessImagesProvider  = brightnessImagesProvider;
        _plant.OnMoistureStatusChanged += OnMoistureStatusChanged;
        _plant.OnBrightnessStatusChanged += OnBrightnessStatusChanged;
        _plant.OnWateringPredictionChanged += OnWateringPredictionChanged;
    }

    private void OnWateringPredictionChanged(TimeSpan timeUntilWateringNeed)
    {
        HoursUntilWateringPrediction = (int)timeUntilWateringNeed.TotalHours;
    }

    private async Task InitializeTamagotchi()
    {
        Name = await _tamagotchiRepository.GetCurrentTamagotchiNameAsync();
        if (Name == null)
        {
            Console.WriteLine("Tamagotchi Name is null");
            throw new ArgumentNullException(Name);
        }
        CurrentPlantType = await _tamagotchiRepository.GetPlantTypeAsync(Name);
        CurrentBrightnessType = await _tamagotchiRepository.GetBrightnessTypeAsync(Name);
        _plant.ChangeMoistureBehaviour(CurrentPlantType);
        _plant.ChangeBrightnessBehaviour(CurrentBrightnessType);

        await _streakBatch.StartScheduling(Name);
        TodaysStreak = await _streakService.GetTodaysStreakAsync(Name);
        var reachedResult = await _streakService.IsGoalReachedAsync(Name);
        if (reachedResult.IsSuccess && reachedResult.Value.HasValue)
        {
            StreakReachedToday = true;
        }
    }
    

    /// <summary>
    /// Change moisture behaviour when plantType changes in the UI
    /// </summary>
    /// <param name="plantType"></param>
    private async Task OnPlantTypeChanged(PlantType plantType)
    {
        _plant.ChangeMoistureBehaviour(plantType);
        await _tamagotchiRepository.UpdatePlantTypeAsync(plantType);
    }

    
    /// <summary>
    /// Change brightness behaviour when brightnessType changes in the UI
    /// </summary>
    /// <param name="brightnessType"></param>
    private async Task OnBrightnessTypeChanged(BrightnessType brightnessType)
    {
        _plant.ChangeBrightnessBehaviour(brightnessType);
        await _tamagotchiRepository.UpdateBrightnessTypeAsync(brightnessType);
    }

    /// <summary>
    /// Change personality type when personalityType changes in the UI
    /// </summary>
    /// <param name="personalityType">new personality</param>
    private async Task OnPersonalityTypeChanged(PersonalityType personalityType)
    {
        _personality = _personalityFactory.Create(personalityType);
        _plant.ForceUpdate();
        await _tamagotchiRepository.UpdatePersonalityType(personalityType);
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
        await _tamagotchiRepository.UpdateNameAsync(tamagotchiName);
    }

    /// <summary>
    /// Calls for interpretation of new brightness statuses
    /// </summary>
    /// <param name="brightnessStatus"></param>
    private void OnBrightnessStatusChanged(BrightnessStatus brightnessStatus)
    {
        CurrentBrightnessStatus = brightnessStatus;
        Console.WriteLine($"Updated BrightnessStatus:  {brightnessStatus}");
        UpdateCurrentMood();
        Dispatcher.UIThread.Post(() =>
        {
            var brightnessImages = _brightnessImagesProvider.ProvideImages(brightnessStatus);
            CurrentBrightnessImages.Clear();
            foreach (var image in brightnessImages)
            {
                CurrentBrightnessImages.Add(GetBitmap(image));
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
        Console.WriteLine($"Updated MoistureStatus:  {status}");
        UpdateCurrentMood();
        Dispatcher.UIThread.Post(() =>
        {
            var moistureImages = _moistureImagesProvider.ProvideImages(status);
            CurrentMoistureImages.Clear();
            foreach (var image in moistureImages)
            {
                CurrentMoistureImages.Add(GetBitmap(image));
            }
        });
    }

    private void UpdateCurrentMood()
    {
        var currentMood = _moodInterpreter.Interpret(CurrentMoistureStatus, CurrentBrightnessStatus);
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
        });
        if (!StreakReachedToday && currentMood == Mood.Happy && DatabaseConnectionFactory.IsInitalized)
        {
            StreakReachedToday = true;
            Dispatcher.UIThread.InvokeAsync(UpdateStreakAsync);
        }
    }

    private async Task UpdateStreakAsync()
    {
        Name ??= await _tamagotchiRepository.GetCurrentTamagotchiNameAsync();
        if (Name == null)
        {
            throw new NullReferenceException("Tamagotchiname ist null");
        }

        var setGoalResult = await _streakService.SetGoalReachedAsync(Name);
        if (setGoalResult.IsFailure)
        {
            switch (setGoalResult.Error)
            {
                case StreakError.GoalAlreadyReached goalAlreadyReached:
                    Console.WriteLine(goalAlreadyReached.Message);
                    break;
                case StreakError.TodayAlreadyExists todayAlreadyExists:
                    Console.WriteLine(todayAlreadyExists.Message);
                    break;
                case StreakError.TodayDoesNotExist todayDoesNotExist: //Fatal
                    throw new ArgumentException(todayDoesNotExist.Message); 
            }
        }
        else
        {
            TodaysStreak++;
        }
    }

    private Bitmap GetBitmap(string image)
    {
        if (_cachedBitmaps.TryGetValue(image, out Bitmap? value))
        {
            return value;
        }
        var uri = new Uri($"{AssetConstants.ASSET_BASE_PATH}{image}");
        var bitmap = new Bitmap(AssetLoader.Open(uri));
        _cachedBitmaps[image] = bitmap;
        return bitmap;
    }
}