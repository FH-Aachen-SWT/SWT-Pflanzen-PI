using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media;
using PflanzenPi.Plants;

namespace PflanzenPi.UI.Tamagotchis.States;

public class MoistureImagesProvider : IMoistureImagesProvider
{
    /// <summary>
    /// 3 Tropfen
    /// Wenn Verywet => 3 Rote ausgefüllte Tropfen
    /// Wenn Wet => 3 ausgefüllte Tropfen
    /// Wenn Satisfied => 2 ausgefüllte Tropfen rest Grau
    /// Wenn Dry => 1 ausgefüllte Tropfen rest Grau
    /// Wenn VeryDry => alle Grau 
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public List<string> ProvideImages(MoistureStatus status)
    {
        const string greyDrop = "greyDrop.png";
        const string drop = "drop.png";
        const string redDrop = "redDrop.png";

        if (status is MoistureStatus.VeryWet)
        {
            return Enumerable.Repeat(redDrop, 3).ToList();
        }

        int drops = (int)status - 1;
        int greyDrops = Math.Max(3 - drops, 0);

        var allDrops = Enumerable.Repeat(drop, drops)
            .Concat(Enumerable.Repeat(greyDrop, greyDrops))
            .ToList();
        return allDrops;
    }
}