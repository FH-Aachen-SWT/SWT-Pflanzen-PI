using Dapper;
using PflanzenPI.Persistence.Database;
using PflanzenPI.Persistence.Schema.Model;

namespace PflanzenPI.Persistence.Repository;

/// <summary>
/// Implementation of tamagotchi repository
/// </summary>
public class TamagotchiRepository : ITamagotchiRepository
{
    public async Task<PlantTypeDB> GetPlantTypeAsync(string name)
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        
        var plantType = await connection.QuerySingleOrDefaultAsync<PlantTypeDB?>("""
                                               SELECT plantType
                                               FROM Tamagotchi 
                                               WHERE name = @Name
                                               """, new { Name = name });
        return plantType ?? PlantTypeDB.MediumWater;
    }

    public async Task UpdatePlantTypeAsync(PlantTypeDB plantType)
    {
        await using var connection =  await DatabaseConnectionFactory.Create();
        await connection.ExecuteScalarAsync("""
                                            UPDATE Tamagotchi
                                            SET plantType = @PlantType 
                                            WHERE isSelected = 1
                                            """, new { PlantType = plantType });
    }

    public async Task UpdateBrightnessTypeAsync(BrightnessTypeDB brightnessType)
    {
        await using var connection =  await DatabaseConnectionFactory.Create();
        await connection.ExecuteScalarAsync("""
                                            UPDATE Tamagotchi
                                            SET brightnessType = @BrightnessType 
                                            WHERE isSelected = 1
                                            """, new { BrightnessType= brightnessType });
    }

    public async Task UpdateNameAsync(string newName)
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        await connection.ExecuteScalarAsync("""
                                            UPDATE Tamagotchi
                                            SET name = @Name
                                            WHERE isSelected = 1
                                            """, new { Name = newName });
    }
    
    public async Task UpdatePersonalityType(PersonalityTypeDB personalityType)
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        await connection.ExecuteScalarAsync("""
                                            UPDATE Tamagotchi
                                            SET personalityType = @PersonalityType
                                            WHERE isSelected = 1
                                            """, new {PersonalityType = personalityType});
    }

    public async Task<BrightnessTypeDB> GetBrightnessTypeAsync(string name)
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        var brightnessType = await connection.QuerySingleOrDefaultAsync<BrightnessTypeDB?>("""
                                                                               SELECT brightnessType 
                                                                               FROM Tamagotchi 
                                                                               WHERE name = @Name
                                                                               """, new { Name = name });
        return brightnessType ?? BrightnessTypeDB.MediumLight;
    }

    public async Task<string?> GetCurrentTamagotchiNameAsync()
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        var name = await connection.QuerySingleOrDefaultAsync<string?>("""
                                                                       SELECT name
                                                                       FROM Tamagotchi
                                                                       WHERE isSelected = 1;
                                                                       """);
        return name;
    }
}