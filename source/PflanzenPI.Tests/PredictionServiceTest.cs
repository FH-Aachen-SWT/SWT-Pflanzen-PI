using FluentAssertions.Extensions;
using Moq;
using PflanzenPi.Plants;
using PflanzenPi.Plants.Behaviours.BrightnessBehaviours;
using PflanzenPi.Plants.Behaviours.MoistureBehaviours;
using PflanzenPi.Plants.PredictionModel;
using PflanzenPi.Sensor.Sensors;
using PflanzenPi.Sensor.Sensors.Mocks;
using Xunit.Abstractions;

namespace PflanzenPI.Tests;

public class PredictionServiceTests
{
    private readonly ITestOutputHelper _output;

    public PredictionServiceTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TestPredictionService()
    {
        var sensorService = new SensorService();
        var moistureBehaviourMockFactory = new Mock<IMoistureBehaviourFactory>();
        var brightnessBehaviourMockFactory = new Mock<IBrightnessBehaviourFactory>();
        var mockBrightnessSensor = new Mock<ISensor<Brightness>>();
        var moistureBehaviourMock = new Mock<IMoistureBehaviour>();
        var moistureSensorMock = new MockMoistureSensorSlowDecline(TimeSpan.FromMilliseconds(1000));

        IPredictionService predictionService =
            new PredictionService(TimeSpan.FromMilliseconds(1000));

        sensorService.Register(moistureSensorMock);
        sensorService.Register(mockBrightnessSensor.Object);

        moistureBehaviourMockFactory
            .Setup(f => f.Create(It.IsAny<PlantType>()))
            .Returns(moistureBehaviourMock.Object);

        var plant = new Plant(
            sensorService,
            moistureBehaviourMockFactory.Object,
            brightnessBehaviourMockFactory.Object,
            predictionService);

        for (int i = 0; i < 70000; i++)
        {
            moistureSensorMock.SimuliereLesen(null);
        }

        var prediction = plant.PredictNextWatering(20);

        if (prediction.HasValue)
            _output.WriteLine($"Prediction: {prediction.Value.TotalSeconds}");
        else
            _output.WriteLine($"Keine valide Prediction");
    }
}
