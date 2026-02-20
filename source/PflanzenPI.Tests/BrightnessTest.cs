using FluentAssertions;
using PflanzenPi.Sensor.Sensors;

namespace PflanzenPI.Tests;

public class BrightnessTest
{
    [Theory]
    [InlineData(-1, true)]
    [InlineData(0, false)]
    [InlineData(double.MaxValue, false)]
    private void TestConstructor(double val, bool shouldThrow)
    {
        var act = () => new Brightness(val);

        if (shouldThrow)
        {
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Muss grösser 0 sein! (Parameter 'data')");
        }
        else
        {
            act.Should().NotThrow();
        }
    }
}