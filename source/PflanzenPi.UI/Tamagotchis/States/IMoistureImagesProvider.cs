using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using PflanzenPi.Plants;
using PflanzenPi.Sensor;

namespace PflanzenPi.UI.Tamagotchis.States;

public interface IMoistureImagesProvider
{
    List<string> ProvideImages(MoistureStatus status);
}