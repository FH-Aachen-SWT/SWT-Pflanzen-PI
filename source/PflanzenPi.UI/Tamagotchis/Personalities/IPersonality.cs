using PflanzenPi.UI.Tamagotchis.Moods;

namespace PflanzenPi.UI.Tamagotchis.Personalities;

public interface IPersonality
{
    string ProvideImage(Mood mood);
}