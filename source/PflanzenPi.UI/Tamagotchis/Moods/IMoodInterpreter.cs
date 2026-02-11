using PflanzenPi.Plants;

namespace PflanzenPi.UI.Tamagotchis.Moods;

public interface IMoodInterpreter
{
    Mood Interpret(MoistureStatus moisture);
}