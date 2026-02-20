using PflanzenPi.Plants.Types;
using PflanzenPI.Persistence.Schema.Model;
using System;

namespace PflanzenPi.UI.Extensions
{
    internal static class PersonalityTypeExtensions
    {
        public static PersonalityTypeDB Map(this PersonalityType personalityType)
        {
            return personalityType switch
            {
                PersonalityType.Neutral => PersonalityTypeDB.Neutral,
                PersonalityType.Happy => PersonalityTypeDB.Happy,
                PersonalityType.Sad => PersonalityTypeDB.Sad,
                _ => throw new NotSupportedException($"No equivalent to {personalityType} in PersonalityTypeDB")
            };
        }

        public static PersonalityType Map(this PersonalityTypeDB personalityTypeDb)
        {
            return personalityTypeDb switch
            {
                PersonalityTypeDB.Neutral => PersonalityType.Neutral,
                PersonalityTypeDB.Happy => PersonalityType.Happy,
                PersonalityTypeDB.Sad => PersonalityType.Sad,
                _ => throw new NotSupportedException($"No equivalent to {personalityTypeDb} in PersonalityType")
            };
        }
    }
}
