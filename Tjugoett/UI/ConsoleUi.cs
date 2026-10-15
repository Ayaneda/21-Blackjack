using Tjugoett.Participants;
using Tjugoett.Rules;

namespace Tjugoett.UI;

public class ConsoleUi
{
    public int ShowMenu() // Shows the menu and reads the choice with ReadInt (below)
    {
        Console.WriteLine();
        Console.WriteLine("=== Tjugoett ===");
        Console.WriteLine("1. Spela en runda");
        Console.WriteLine("2. Visa tidigare resultat");
        Console.WriteLine("3. Avsluta");
        Console.Write("Välj: ");
        return ReadInt(1, 3);
    }

    public int AskPlayerCount() // Asks how many players, 1 to 5
    {
        Console.Write("Ange antal spelare (1-5): ");
        return ReadInt(1, 5);
    }

    public string AskName()
    {
        Console.Write("Namn på spelare: ");
        return Console.ReadLine() ?? "Spelare"; // If null, the name is "Spelare"
    }

    public Move AskMove() //Asks the player for hit or stand
    {
        Console.WriteLine("1. Ta kort (Hit)");
        Console.WriteLine("2. Stanna (Stand)");
        Console.Write("Välj: ");
        int choice = ReadInt(1, 2);

        if (choice == 1)
        {
            return Move.Hit;
        }
        return Move.Stand;
    }


    public void ShowHand(Participant participant, bool hideHoleCard) //Print the cards and the total, hide the hole card if hideHoleCard is true. 
    {                                                                   //DANIAL, fortsätt!

    }

    public void ShowMessage(string message) //Method that is used by Game.cs to print messages
    {
        Console.WriteLine(message);
    }

    public void ShowOutcome(Outcome outcome)// Prints how the round ended. DANIAL, fortsätt!
    {

    }

    private int ReadInt(int min, int max) // Reads a number between min and max, asks again if invalid. Is used in ShowMenu and AskPlayerCount
    {
        while (true)
        {
            string input = Console.ReadLine() ?? "";
            if (int.TryParse(input, out int number) && number >= min && number <= max)
            {
                return number;
            }
            Console.Write($"Skriv en siffra mellan {min} och {max}: ");
        }
    }
}