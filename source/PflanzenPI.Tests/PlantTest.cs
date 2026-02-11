using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;
using Moq;
using PflanzenPi.Plants;
using PflanzenPi.Sensor;

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
        var moistureBehaviourMock = new Mock<IMoistureBehaviour>();
        var moistureSensorMock = new Mock<ISensor<Moisture>>();
        
        sensorService.Register(moistureSensorMock.Object);
        moistureBehaviourMock.Setup(s => s.Interpret(previous)).Returns(MoistureStatus.Dry);
        moistureBehaviourMock.Setup(s => s.Interpret(nexti)).Returns(MoistureStatus.Satisfied);

        var plant = new Plant(sensorService, moistureBehaviourMock.Object);
        var eventRaised = false;

        plant.OnMoistureStatusChanged += (MoistureStatus) =>
        {
            eventRaised = true;
        };
        
        plant.UpdateMoisture(previous, nexti);

        eventRaised.Should().Be(invoke);
    }
}
