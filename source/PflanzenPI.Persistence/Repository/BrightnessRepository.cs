using Dapper;
using PflanzenPI.Persistence.Database;
using PflanzenPI.Persistence.Schema;

namespace PflanzenPI.Persistence.Repository;

public class BrightnessRepository : IBrightnessRepository
{
    public async Task<BrightnessEntity?> GetBrightnessByHourAsync(int hour)
    {
        var hourColumn = "hour" + hour;
        await using var connection = await DatabaseConnectionFactory.Create();
        var brightnessEntity = await connection.QuerySingleOrDefaultAsync<BrightnessEntity>($"""
            SELECT owner, {hourColumn} AS hourLux
            FROM Brightness
            WHERE owner = (SELECT name FROM Tamagotchi WHERE isSelected = 1 LIMIT 1)
            """);

        return brightnessEntity;
    }

    public async Task UpdateBrightnessByHourAsync(int hour, double lux)
    {
        var hourColumn = "hour" + hour;
        await using var connection = await DatabaseConnectionFactory.Create();
        await connection.ExecuteScalarAsync($"""
                                            UPDATE Brightness
                                            SET {hourColumn} = @Lux
                                            WHERE owner = (SELECT name FROM Tamagotchi WHERE isSelected = 1 LIMIT 1)
                                            """, new {Lux = lux});
    }

    public async Task AddTamagotchiToBrightnessAsync()
    {
        await using var connection = await DatabaseConnectionFactory.Create();
        await connection.ExecuteAsync("""
                                            INSERT INTO Brightness(owner, hour1, hour2, hour3, hour4, hour5, hour6, hour7, hour8, hour9, hour10, hour11, hour12, hour13, hour14, hour15, hour16, hour17, hour18, hour19, hour20, hour21, hour22, hour23, hour24)
                                            VALUES (
                                                (SELECT name FROM Tamagotchi WHERE isSelected = 1 LIMIT 1),
                                                3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000
                                            );
                                            """);
    }
}