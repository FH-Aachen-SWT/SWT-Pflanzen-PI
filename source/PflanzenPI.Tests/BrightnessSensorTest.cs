using FluentAssertions;
using Moq;
using PflanzenPI.Persistence.Business;
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
        var serviceMock = new Mock<IBrightnessService>();

        serviceMock.Setup(s => s.GetDLIAsync()).ReturnsAsync(5);
        
        var ms = new BrightnessSensor(interval, serviceMock.Object, adc.Object);

        await Task.Delay(TimeSpan.FromSeconds(2));

        ms.Current.Should().BeEquivalentTo(new Brightness(5));
    }
}