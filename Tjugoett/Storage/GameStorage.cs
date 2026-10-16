
using Tjugoett.Rules;
namespace Tjugoett.Storage;

public class GameStorage
{
    private readonly string _filePath = "results.txt"; // File where player names and game results are stored.

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
}
