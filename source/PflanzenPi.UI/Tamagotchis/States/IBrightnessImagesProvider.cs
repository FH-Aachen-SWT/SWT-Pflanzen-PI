using System.Collections.Generic;
using PflanzenPi.Plants;

namespace PflanzenPi.UI.Tamagotchis.States;

public interface IBrightnessImagesProvider
{
    List<string> ProvideImages(BrightnesStatus status);
}