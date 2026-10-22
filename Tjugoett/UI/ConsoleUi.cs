using Tjugoett.Participants;
using Tjugoett.Rules;
using Tjugoett.Cards;

namespace Tjugoett.UI;

public class ConsoleUi
{
    public int ShowMenu() // Shows the menu and reads the choice
    {
        Console.WriteLine();
        Console.WriteLine("=== Tjugoett ===");
        Console.WriteLine("1. Spela en runda");
        Console.WriteLine("2. Visa tidigare resultat");
        Console.WriteLine("3. Avsluta");
        Console.Write("Välj: ");
        string input = Console.ReadLine() ?? "";
        int choice;

        while (!int.TryParse(input, out choice) || choice < 1 || choice > 3) // Ask again if it is not a number between 1 and 3
        {
            Console.Write("Skriv en siffra mellan 1 och 3: ");
            input = Console.ReadLine() ?? "";
        }
        return choice;
    }

    public int AskPlayerCount() // Asks how many players, 1 to 5
    {
        Console.Write("Ange antal spelare (1-5): ");
        string input = Console.ReadLine() ?? "";
        int count;

        while (!int.TryParse(input, out count) || count < 1 || count > 5) // Ask again if it is not a number between 1 and 5
        {
            Console.Write("Skriv en siffra mellan 1 och 5: ");
            input = Console.ReadLine() ?? "";
        }
        return count;
    }

    public int AskComputerPlayerCount(int max) // Asks how many computer players, 0 to max (max depends on the number of human players that are playing)
    {
        Console.Write($"Ange antal datorspelare (0-{max}): ");
        string input = Console.ReadLine() ?? "";
        int count;

        while (!int.TryParse(input, out count) || count < 0 || count > max) // Ask again if it is not a number between 0 and max
        {
            Console.Write($"Skriv en siffra mellan 0 och {max}: ");
            input = Console.ReadLine() ?? "";
        }
        return count;
    }

    public string AskName()
    {
        Console.Write("Namn på spelare: ");
        return Console.ReadLine() ?? "Spelare"; // If null, the name is "Spelare"
    }
    public decimal AskBet(Player player) // Asks how many chips the player wants to bet.
    {
        Console.WriteLine();
        Console.WriteLine($"{player.Name} har {player.Betting.Chips} marker.");
        Console.Write("Hur mycket vill du satsa? ");

        while (true) // Keeps asking until the bet is valid.
        {
            string input = Console.ReadLine() ?? "";

            if (decimal.TryParse(input, out decimal amount) &&
                amount > 0 && amount <= player.Betting.Chips)
            {
                return amount; // Returns the valid bet.
            }

            Console.Write("Ogiltig satsning. Försök igen: ");
        }
    }
    public Move AskMove(Participant participant, Dealer dealer, List<Player> players) //Shows the dealer's open card and all players' hands, then asks for hit or stand
    {
        ShowHand(dealer, dealer.HoleCardHidden);
        Console.WriteLine();
        for (int i = 0; i < players.Count; i++) // Show every player's cards at the table
        {
            ShowHand(players[i], false);
            Console.WriteLine();
        }
        Console.WriteLine($"Det är {participant.Name}s tur:"); // So you can see whose turn it is
        Console.WriteLine("1. Ta kort (Hit)");
        Console.WriteLine("2. Stanna (Stand)");
        Console.Write("Välj: ");
        string input = Console.ReadLine() ?? "";
        int choice;

        while (!int.TryParse(input, out choice) || choice < 1 || choice > 2) // Ask again if it is not a number between 1 and 2
        {
            Console.Write("Skriv en siffra mellan 1 och 2: ");
            input = Console.ReadLine() ?? "";
        }

        if (choice == 1)
        {
            return Move.Hit;
        }
        return Move.Stand;
    }


    public void ShowHand(Participant participant, bool hideHoleCard) //Print the cards and the total, hide the hole card if hideHoleCard is true. 
    {
        Console.WriteLine($"{participant.Name}:");

        for (int row = 0; row < 7; row++) // This goes throw all 7 rows(every time that a row is writen go to the next row of the card)
        {
            for (int i = 0; i < participant.Hand.Cards.Count; i++) // This goes throw all cards in hand
            {
                string cardPicture;
                if (hideHoleCard && i == 1) // If bool is active, hide a card from the dealer
                {
                    cardPicture = ShowCard.ShowHideCard();
                }
                else
                {
                    cardPicture = participant.Hand.Cards[i].ToString(); // This show the cards normally
                }
                string[] cardLines = cardPicture.Split('\n'); // This split the card when \n reach 
                Console.Write(cardLines[row] + " ");          // Print only the current row + space between first row of all cards
            }
            Console.WriteLine(); // This just create a new empty line
        }
        if (!hideHoleCard) // Show the total only when all cards are visible.
        {
            Console.WriteLine($"Värde: {participant.Hand.Value}");
        }

    }

    public void ShowMessage(string message) //Method that is used by Game.cs to print messages
    {
        Console.WriteLine(message);
    }

    public string GetInput() // Reads a line of text from the user. Is used by Game.cs
    {
        return Console.ReadLine() ?? "";
    }

    public void Pause() // Short pause so the user can follow the computer players
    {
        Thread.Sleep(1000); // 1000 milliseconds = 1 second
    }

    public void ShowOutcome(Outcome outcome)// Prints how the round ended. 
    {
        if (outcome == Outcome.Win)
        {
            Console.WriteLine(@"\(^_^)/");
            Console.WriteLine("Du vann!");
        }
        else if (outcome == Outcome.Lose)
        {
            Console.WriteLine("(╥﹏╥)");
            Console.WriteLine("Du förlorade.");
        }
        else if (outcome == Outcome.Push)
        {
            Console.WriteLine("¯\\_(ツ)_/¯");
            Console.WriteLine("Det blev lika.");
        }
        else if (outcome == Outcome.Blackjack)
        {
            Console.WriteLine("★ ★ ★");
            Console.WriteLine("Naturlig tjugoett!");
        }

    }
}