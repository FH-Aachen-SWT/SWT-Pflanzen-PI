using Dapper;
using PflanzenPI.Persistence.Database;
using PflanzenPi.Plants;
using PflanzenPi.Plants.Types;

namespace PflanzenPI.Persistence.Repository;

/// <summary>
/// Implementation of tamagotchi repository
/// </summary>
public class TamagotchiRepository : ITamagotchiRepository
{
    public async Task<PlantType> GetPlantType(string name)
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        
        var plantType = await connection.QuerySingleOrDefaultAsync<PlantType?>("""
                                               SELECT plantType
                                               FROM Tamagotchi 
                                               WHERE name = @Name
                                               """, new { Name = name });
        return plantType ?? PlantType.MediumWater;
    }

    public async Task UpdatePlantType(PlantType plantType)
    {
        await using var connection =  await DatabaseConnectionFactory.Create();
        await connection.ExecuteScalarAsync("""
                                            UPDATE Tamagotchi
                                            SET plantType = @PlantType 
                                            WHERE isSelected = 1
                                            """, new { PlantType = plantType });
    }

    public async Task UpdateBrightnessType(BrightnessType brightnessType)
    {
        await using var connection =  await DatabaseConnectionFactory.Create();
        await connection.ExecuteScalarAsync("""
                                            UPDATE Tamagotchi
                                            SET brightnessType = @BrightnessType 
                                            WHERE isSelected = 1
                                            """, new { BrightnessType= brightnessType });
    }

    public async Task UpdatePersonalityType(PersonalityType personalityType)
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        await connection.ExecuteScalarAsync("""
                                            UPDATE Tamagotchi
                                            SET personalityType = @PersonalityType
                                            WHERE isSelected = 1
                                            """, new {PersonalityType = personalityType});
    }

    public async Task UpdateName(string newName)
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        await connection.ExecuteScalarAsync("""
                                            UPDATE Tamagotchi
                                            SET name = @Name
                                            WHERE isSelected = 1
                                            """, new { Name = newName });
    }

    public async Task<BrightnessType> GetBrightnessType(string name)
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        var brightnessType = await connection.QuerySingleOrDefaultAsync<BrightnessType?>("""
                                                                               SELECT brightnessType 
                                                                               FROM Tamagotchi 
                                                                               WHERE name = @Name
                                                                               """, new { Name = name });
        return brightnessType ?? BrightnessType.MediumLight;
    }

    public async Task<string> GetCurrentTamagotchiName()
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        var name = await connection.QuerySingleOrDefaultAsync<string?>("""
                                                                       SELECT name
                                                                       FROM Tamagotchi
                                                                       WHERE isSelected = 1;
                                                                       """);
        return name ?? "Bob.B";
    }
}