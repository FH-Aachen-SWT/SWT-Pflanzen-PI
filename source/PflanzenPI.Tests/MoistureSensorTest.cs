using FluentAssertions;
using Iot.Device.Ads1115;
using Moq;
using PflanzenPi.Sensor.Sensors;
using PflanzenPi.Sensor.Sensors.Adapter;

namespace PflanzenPI.Tests;

public class MoistureSensorTest
{
    [Fact]
    private async Task TestReadFromPi()
    {
        var adc = new Mock<II2CAdapter>();
        adc.Setup(s => s.ReadRawShort()).Returns(17480);
        TimeSpan interval = TimeSpan.FromSeconds(1);
        var ms = new MoistureSensor(interval, adc.Object);

        await Task.Delay(TimeSpan.FromSeconds(2));

        ms.Current.Should().BeEquivalentTo(new Moisture(1.5757446F));
    }
}