using FluentAssertions;
using Moq;
using PflanzenPi.Plants;
using PflanzenPi.Plants.Behaviours.BrightnessBehaviours;
using PflanzenPi.Plants.Behaviours.MoistureBehaviours;
using PflanzenPi.Plants.PredictionModel;
using PflanzenPi.Plants.Types;
using PflanzenPi.Sensor;
using PflanzenPi.Sensor.Sensors;

namespace PflanzenPI.Tests;

public class PlantTest
{
    [Theory]
    [InlineData(null, 0, true)]
    [InlineData(0, 100, true)]
    public void testUpdateMoisture(float prev, float next, bool invoke)
    {
        Moisture previous = new Moisture(prev);
        Moisture nexti = new Moisture(next);
        var sensorService = new SensorService();
        var moistureBehaviourMockFactory = new Mock<IMoistureBehaviourFactory>();
        var brightnessBehaviourMockFactory = new Mock<IBrightnessBehaviourFactory>();
        var mockBrightnessSensor = new Mock<ISensor<Brightness>>();
        var moistureBehaviourMock = new Mock<IMoistureBehaviour>();
        var moistureSensorMock = new Mock<ISensor<Moisture>>();
        
        IPredictionService predictionService = new PredictionService(TimeSpan.FromMilliseconds(1000), 10, 10);
        
        sensorService.Register(moistureSensorMock.Object);
        sensorService.Register(mockBrightnessSensor.Object);
        moistureBehaviourMockFactory.Setup(factory => factory.Create(It.IsAny<PlantType>())).Returns(moistureBehaviourMock.Object);
        moistureBehaviourMock.Setup(s => s.Interpret(previous)).Returns(MoistureStatus.Dry);
        moistureBehaviourMock.Setup(s => s.Interpret(nexti)).Returns(MoistureStatus.Satisfied);

        var plant = new Plant(sensorService, moistureBehaviourMockFactory.Object,  brightnessBehaviourMockFactory.Object, predictionService);
        var eventRaised = false;

        plant.OnMoistureStatusChanged += (MoistureStatus) =>
        {
            eventRaised = true;
        };
        
        plant.UpdateMoisture(previous, nexti);

        eventRaised.Should().Be(invoke);
    }
}
