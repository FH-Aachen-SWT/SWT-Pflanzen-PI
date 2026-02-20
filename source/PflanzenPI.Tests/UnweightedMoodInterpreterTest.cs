using FluentAssertions;
using PflanzenPi.Plants;
using PflanzenPi.UI.Tamagotchis.Moods;

namespace PflanzenPI.Tests;

public class UnweightedMoodInterpreterTest
{
    private UnweightedMoodInterpreter tested = new UnweightedMoodInterpreter();
    
    [Theory]
    [InlineData(MoistureStatus.VeryDry, BrightnessStatus.VeryLow, Mood.Angry)]
    [InlineData(MoistureStatus.VeryWet, BrightnessStatus.High, Mood.Angry)]
    [InlineData(MoistureStatus.Satisfied, BrightnessStatus.Low, Mood.Happy)]
    [InlineData(MoistureStatus.Wet, BrightnessStatus.Satisfied, Mood.Happy)]
    [InlineData(MoistureStatus.Wet, BrightnessStatus.Medium, Mood.Neutral)]
    [InlineData(MoistureStatus.VeryDry, BrightnessStatus.Low, Mood.Sad)]
    [InlineData(MoistureStatus.Satisfied, BrightnessStatus.Satisfied, Mood.Happy)]
    private void TestInterpret(MoistureStatus moisture, BrightnessStatus brightness, Mood expectedMood)
    {
        var actual = tested.Interpret(moisture, brightness);

        actual.Should().HaveSameValueAs(expectedMood);
    }
}