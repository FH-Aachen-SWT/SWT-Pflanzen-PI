using PflanzenPi.Plants.Types;
using PflanzenPI.Persistence.Schema.Model;
using System;

namespace PflanzenPi.Plants.Extensions
{
    internal static class PlantTypeExtensions
    {
        public static PlantTypeDB Map(this PlantType plantType)
        {
            return plantType switch
            {
                PlantType.LittleWater => PlantTypeDB.LittleWater,
                PlantType.MediumWater => PlantTypeDB.MediumWater,
                PlantType.MuchWater => PlantTypeDB.MuchWater,
                _ => throw new NotSupportedException($"No equivalent to {plantType} in PlantTypeDB")
            };
        }

        public static PlantType Map(this PlantTypeDB plantTypeDb)
        {
            return plantTypeDb switch
            {
                PlantTypeDB.LittleWater => PlantType.LittleWater,
                PlantTypeDB.MediumWater => PlantType.MediumWater,
                PlantTypeDB.MuchWater => PlantType.MuchWater,
                _ => throw new NotSupportedException($"No equivalent to {plantTypeDb} in PlantType")
            };
        }
    }
}
