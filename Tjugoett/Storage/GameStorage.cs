
using Tjugoett.Rules;
namespace Tjugoett.Storage;

public class GameStorage
{
    private readonly string _filePath = "results.txt"; // File where player names and game results are stored.
    private readonly string _chipsFilePath = "chips.txt"; // Store player chip balances.
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
    public void SaveChips(string playerName, int chips) // Save player chips.
    {
        List<string> lines = new List<string>(); // Stroe save data.
        bool found = false;
        if (File.Exists(_chipsFilePath)) // Check if file exists.
        {
            lines = File.ReadAllLines(_chipsFilePath).ToList();
        }
        for (int i = 0; i < lines.Count; i++) // Check existing players.
        {
            string[] parts = lines[i].Split(';');
            if (parts.Length == 2 && parts[0] == playerName)
            {
                lines[i] = $"{playerName};{chips}"; // Update chips.
                found = true;
                break;
            }

        }
        if (!found) // Add a new player
        {
            lines.Add($"{playerName};{chips}");
        }
        File.WriteAllLines(_chipsFilePath, lines); // Save to fils.
    }
    public int LoadChips(string playerName) // Load saved player chips.
    {
        if (!File.Exists(_chipsFilePath)) // Chieck if file exists.
        {
            return 100; // Default starting chips.
        }
        foreach (string line in File.ReadAllLines(_chipsFilePath))
        {
            string[] parts = line.Split(';');
            if (parts.Length == 2 && parts[0] == playerName)
            {
                if (int.TryParse(parts[1], out int chips) && chips >= 0)
                {
                    return chips; // Return saved chips.
                }
            }
        }
        return 100; // New players start with 100 chips.
    }

}
