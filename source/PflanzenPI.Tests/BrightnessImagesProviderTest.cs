using FluentAssertions;
using PflanzenPi.Plants;
using PflanzenPi.UI.Tamagotchis.States;

namespace PflanzenPI.Tests;

public class BrightnessImagesProviderTest
{
    private const string greySun = "greySun.png";
    private const string sun = "sun.png";
    private const string redSun = "redSun.png";

    private IBrightnessImagesProvider tested = new BrightnessImagesProvider();
    
    [Theory]
    [InlineData(redSun, redSun, redSun, BrightnessStatus.High)]
    [InlineData(sun, sun, sun, BrightnessStatus.Medium)]
    [InlineData(sun, sun, greySun, BrightnessStatus.Satisfied)]
    [InlineData(sun, greySun, greySun, BrightnessStatus.Low)]
    [InlineData(greySun, greySun, greySun, BrightnessStatus.VeryLow)]
    private void TestProvideImages(string bild1, string bild2, string bild3, BrightnessStatus status)
    {
        List<string> expected = [bild1, bild2, bild3];
        var actual = tested.ProvideImages(status);

        actual.Should().BeEquivalentTo(expected);
    }
}