using FluentAssertions;
using PflanzenPi.Sensor;

namespace PflanzenPI.Tests;

public class MoistureTest
{
    [Theory]
    [InlineData(0, false)]
    [InlineData(-1, true)]
    [InlineData(-2, true)]
    [InlineData(50, false)]
    [InlineData(100, false)]
    [InlineData(101, true)]
    [InlineData(102, true)]
    private void TestMoisture(float data, bool shouldThrow)
    {
        var act = () => new Moisture(data);

        if (shouldThrow)
        {
            act.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Keinen gültigen Wert von der PI gelesen (Parameter 'data')");
        }
        else
        {
            act.Should().NotThrow();
        }
    }
}