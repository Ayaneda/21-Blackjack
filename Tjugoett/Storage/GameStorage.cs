namespace Tjugoett.Storage;

public class GameStorage
{
    private readonly string _filePath = "results.text"; // File where player names and game results are stored.

    public void SaveResult(string playerName, Outcome outcome) // Sve a plyyer's result without deleting previous results.
    {
        string result = $"{playerName} - {outcome}";
        File.AppendAllText(_filePath, result + Environment.NewLine);
    }

}
