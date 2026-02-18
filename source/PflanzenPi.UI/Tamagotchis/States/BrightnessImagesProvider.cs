using System;
using System.Collections.Generic;
using System.Linq;
using PflanzenPi.Plants;

namespace PflanzenPi.UI.Tamagotchis.States;

public class BrightnessImagesProvider : IBrightnessImagesProvider
{
    /// <summary>
    /// 3 Sonnen
    /// Wenn High => 3 Rote ausgefüllte Sonnen
    /// Wenn Medium => 3 ausgefüllte Sonnen
    /// Wenn Satisfied => 2 ausgefüllte Sonnen rest Grau
    /// Wenn Low => 1 ausgefüllte Sonnen rest Grau
    /// Wenn VeryLow => alle Grau 
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public List<string> ProvideImages(BrightnessStatus status)
    {
        const string greySun = "greySun.png";
        const string sun = "sun.png";
        const string redSun = "redSun.png";

        if (status is BrightnessStatus.High)
        {
            return Enumerable.Repeat(redSun, 3).ToList();
        }

        int suns = Math.Max((int)status, 0);
        int greySuns = Math.Max(3 - suns, 0);

        var allSuns = Enumerable.Repeat(sun, suns)
            .Concat(Enumerable.Repeat(greySun, greySuns))
            .ToList();
        return allSuns;
    }
}