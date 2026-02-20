using FluentAssertions;
using Moq;
using PflanzenPi.Sensor.Sensors;
using PflanzenPi.Sensor.Sensors.Adapter;

namespace PflanzenPI.Tests;

public class BrightnessSensorTest
{
    [Fact]
    private async Task TestReadFromPi()
    {
        var adc = new Mock<II2CAdapter>();
        adc.Setup(s => s.ReadRawDouble()).Returns(500);
        TimeSpan interval = TimeSpan.FromSeconds(1);
        var ms = new BrightnessSensor(interval, adc.Object);

        await Task.Delay(TimeSpan.FromSeconds(2));

        ms.Current.Should().BeEquivalentTo(new Brightness(500));
    }
}