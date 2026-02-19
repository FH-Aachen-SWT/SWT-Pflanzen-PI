using FluentAssertions;
using PflanzenPi.Plants;
using PflanzenPi.UI.Tamagotchis.States;

namespace PflanzenPI.Tests;

public class MoistureImagesProviderTest
{
    private const string greyDrop = "greyDrop.png";
    private const string drop = "drop.png";
    private const string redDrop = "redDrop.png";

    private IMoistureImagesProvider tested = new MoistureImagesProvider();

    [Theory]
    [InlineData(redDrop, redDrop, redDrop, MoistureStatus.VeryWet)]
    [InlineData(drop, drop, drop, MoistureStatus.Wet)]
    [InlineData(drop, drop, greyDrop, MoistureStatus.Satisfied)]
    [InlineData(drop, greyDrop, greyDrop, MoistureStatus.Dry)]
    [InlineData(greyDrop, greyDrop, greyDrop, MoistureStatus.VeryDry)]
    private void TestProvideImages(string bild1, string bild2, string bild3, MoistureStatus status)
    {
        List<string> expected = [bild1, bild2, bild3];
        var actual = tested.ProvideImages(status);

        actual.Should().BeEquivalentTo(expected);
    }
}