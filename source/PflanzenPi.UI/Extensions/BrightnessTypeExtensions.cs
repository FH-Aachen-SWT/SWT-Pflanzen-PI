using PflanzenPi.Plants.Types;
using PflanzenPI.Persistence.Schema.Model;
using System;

namespace PflanzenPi.UI.Extensions
{
    internal static class BrightnessTypeExtensions
    {
        public static BrightnessTypeDB Map(this BrightnessType brightnessType)
        {
            return brightnessType switch
            {
                BrightnessType.LittleLight => BrightnessTypeDB.LittleLight,
                BrightnessType.MediumLight => BrightnessTypeDB.MediumLight,
                BrightnessType.MuchLight => BrightnessTypeDB.MuchLight,
                _ => throw new NotSupportedException($"No equivalent to {brightnessType} in BrightnessTypeDB")
            };
        }

        public static BrightnessType Map(this BrightnessTypeDB brightnessTypeDb)
        {
            return brightnessTypeDb switch
            {
                BrightnessTypeDB.LittleLight => BrightnessType.LittleLight,
                BrightnessTypeDB.MediumLight => BrightnessType.MediumLight,
                BrightnessTypeDB.MuchLight => BrightnessType.MuchLight,
                _ => throw new NotSupportedException($"No equivalent to {brightnessTypeDb} in BrightnessType")
            };
        }
    }
}
