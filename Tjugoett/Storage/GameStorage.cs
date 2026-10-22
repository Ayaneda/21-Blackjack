
using Tjugoett.Rules;
namespace Tjugoett.Storage;

public class GameStorage
{
    private readonly string _filePath = "results.txt"; // File where player names and game results are stored.

    private readonly string _playersFilePath = "players.txt"; // File for saving the last active players.
    public void SaveResult(string playerName, Outcome outcome) // Save a plyyer's result without deleting previous results.
    {
        string result = $"{playerName} - {outcome}";
        File.AppendAllText(_filePath, result + Environment.NewLine);
    }
    public List<string> LoadResults() // Load all previously saved results from the file.
    {
        if (!File.Exists(_filePath)) // Return an empty list if the file does not exist.
        {
            return new List<string>();
        }
        return File.ReadAllLines(_filePath).ToList();
    }
    public Dictionary<string, int[]> LoadPlayerStats() // Läser in spelarnas statistik.
    {
        Dictionary<string, int[]> stats = new();

        foreach (string result in LoadResults()) // Går igenom alla sparade resultat.
        {
            string[] parts = result.Split(" - "); // Delar upp namn och resultat.

            if (parts.Length != 2) continue; // Hoppar över felaktiga rader.

            string name = parts[0];
            string outcome = parts[1];

            if (!stats.ContainsKey(name)) // Kontrollerar om spelaren finns.
                stats[name] = new int[3]; // Skapar plats för vinster, förluster och oavgjorda.

            if (outcome == "Win" || outcome == "Blackjack")
                stats[name][0]++; // Räknar vinster.

            else if (outcome == "Lose")
                stats[name][1]++; // Räknar förluster.

            else if (outcome == "Push")
                stats[name][2]++; // Räknar oavgjorda.
        }

        return stats; // Returnerar spelarnas statistik.
    }
    public List<string> LoadPlayerNames() // Loads previously saved player names.
    {
        List<string> names = new();

        foreach (string name in LoadPlayerStats().Keys) // Goes through saved player names.
        {
            if (!name.StartsWith("Dator ")) // Skips computer players.
            {
                names.Add(name); // Adds the player to the list.
            }
        }

        return names; // Returns the player names.
    }
    public void SavePlayers(List<string> names) // Saves the last active players.
    {
        File.WriteAllLines(_playersFilePath, names); // Saves each name on a separate line.
    }
    public List<string> LoadSavedPlayers() // Loads the last active players.
    {
        if (!File.Exists(_playersFilePath)) // Check if the file exists.
        {
            return new List<string>();
        }

        return File.ReadAllLines(_playersFilePath).ToList(); // Read saved player names.
    }
}
