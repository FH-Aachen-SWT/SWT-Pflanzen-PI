using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using FluentAssertions;
using Moq;
using PflanzenPi.Plants;
using PflanzenPi.Plants.Behaviours.BrightnessBehaviours;
using PflanzenPi.Plants.Behaviours.MoistureBehaviours;
using PflanzenPi.Sensor;
using PflanzenPi.UI;
using PflanzenPi.UI.Tamagotchis;
using PflanzenPi.UI.Tamagotchis.Moods;
using PflanzenPi.UI.Tamagotchis.Personalities;
using PflanzenPi.UI.Tamagotchis.States;
using PflanzenPi.UI.Viewmodel;

namespace PflanzenPI.Tests;

public class TamagotchiTest
{
    [Fact]
    private void TestTamagotchi()
    {
        // Kann hier nur testen, dass keine Exception ausgelöst wird
        
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .SetupWithoutStarting();
        
        var sensorService = new SensorService();
        var moistureBehaviourMockFactory = new Mock<IMoistureBehaviourFactory>();
        var brightnessBehaviourMockFactory = new Mock<IBrightnessBehaviourFactory>();
        var mockBrightnessSensor = new Mock<ISensor<Brightness>>();
        var moistureBehaviourMock = new Mock<IMoistureBehaviour>();
        var moistureSensorMock = new Mock<ISensor<Moisture>>();
        
        sensorService.Register(moistureSensorMock.Object);
        sensorService.Register(mockBrightnessSensor.Object);
        moistureBehaviourMockFactory.Setup(factory => factory.Create(It.IsAny<PlantType>())).Returns(moistureBehaviourMock.Object);

        var plant = new Plant(sensorService, moistureBehaviourMockFactory.Object,
            brightnessBehaviourMockFactory.Object);

        var moodInterpreterMock = new Mock<IMoodInterpreter>();
        var personalityMock = new Mock<IPersonality>();
        var moistureImagesProviderMock = new Mock<IMoistureImagesProvider>();
        var brightnessImagesProviderMock = new Mock<IBrightnessImagesProvider>();

        var status = MoistureStatus.Satisfied;
        var mood = Mood.Happy;
        var moodImageName = "satisfied.gif";
        
        // Antworten für OnMoistureStatusChanged
        moodInterpreterMock.Setup(s => s.Interpret(status)).Returns(mood);
        personalityMock.Setup(s => s.ProvideImage(mood)).Returns(moodImageName);
        moistureImagesProviderMock.Setup(s => s.ProvideImages(status)).Returns(["drop.png", "drop.png", "drop.png"]);
        
        // Antworten für OnBrightnessStatusChanged
        brightnessImagesProviderMock.Setup(s => s.ProvideImages(BrightnessStatus.Satisfied))
            .Returns(["sun.png", "sun.png", "sun.png"]);

        var tamagotchi = () => new Tamagotchi(plant, moodInterpreterMock.Object, personalityMock.Object,
            moistureImagesProviderMock.Object, brightnessImagesProviderMock.Object);

        tamagotchi.Should().NotThrow();
    }
}