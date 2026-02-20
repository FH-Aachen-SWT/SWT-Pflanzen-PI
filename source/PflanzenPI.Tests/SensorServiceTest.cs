using FluentAssertions;
using Moq;
using PflanzenPi.Sensor.Sensors;
using PflanzenPi.Sensor.Sensors.Adapter;

namespace PflanzenPI.Tests;

public class SensorServiceTest
{
    [Fact]
    private void TestRegisterAndGet()
    {
        var adc = new Mock<II2CAdapter>();
        var tested = new SensorService();
        var ms = new MoistureSensor(TimeSpan.FromSeconds(1), adc.Object);
        
        tested.Register(ms);

        tested.Get<Moisture>().Should().Be(ms);
    }
}