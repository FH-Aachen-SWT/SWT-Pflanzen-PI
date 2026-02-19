using FluentAssertions;
using PflanzenPi.Plants;
using PflanzenPi.UI.Tamagotchis.Moods;

namespace PflanzenPI.Tests;

public class ExtremesMoodInterpreterTest
{
    private ExtremesMoodInterpreter tested = new ExtremesMoodInterpreter();
    
    [Theory]
    [InlineData(MoistureStatus.VeryDry, BrightnessStatus.VeryLow, Mood.Angry)]
    [InlineData(MoistureStatus.VeryWet, BrightnessStatus.High, Mood.Angry)]
    [InlineData(MoistureStatus.Satisfied, BrightnessStatus.Low, Mood.Neutral)]
    [InlineData(MoistureStatus.Wet, BrightnessStatus.Satisfied, Mood.Sad)]
    [InlineData(MoistureStatus.Wet, BrightnessStatus.Medium, Mood.Sad)]
    [InlineData(MoistureStatus.VeryDry, BrightnessStatus.Low, Mood.Angry)]
    [InlineData(MoistureStatus.Satisfied, BrightnessStatus.Satisfied, Mood.Happy)]
    private void TestInterpret(MoistureStatus moisture, BrightnessStatus brightness, Mood expectedMood)
    {
        var actual = tested.Interpret(moisture, brightness);

        actual.Should().HaveSameValueAs(expectedMood);
    }
}