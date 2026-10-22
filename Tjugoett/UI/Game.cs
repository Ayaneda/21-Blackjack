
using Tjugoett.Cards;
using Tjugoett.Participants;
using Tjugoett.Rules;
using Tjugoett.Storage;
using Tjugoett.Strategies;

namespace Tjugoett.UI;

public class Game
{
    private Shoes _deck;
    private List<Player> _players;
    private Dealer _dealer;
    private ConsoleUi _ui;
    private Round _round;
    private GameStorage _storage;

    public Game() // This is the constructor for the class Game
    {
        _deck = new Shoes(6); // Creates a shoe with six decks.
        _players = new List<Player>();
        _dealer = new Dealer("Dealer");
        _ui = new ConsoleUi();
        _round = new Round();
        _storage = new GameStorage();
    }

    public void Run() // this Will run the game
    {
        bool isRunning = true; // a bool to stop the game when is change

        while (isRunning)
        {
            int choice = _ui.ShowMenu();

            switch (choice)
            {
                case 1:
                    PlayRound(); // This will run the method for
                    break;

                case 2:
                    ShowResults(); // Show all save data
                    break;
                case 3:
                    isRunning = false;
                    break;
            }
        }
    }

    private void PlayRound()
    {
        if (_players.Count == 0) // The first time will ask for players because there is not player yet
        {
            // Load the players from the last game.
            List<string> lastPlayers = _storage.LoadSavedPlayers();
            if (lastPlayers.Count > 0 && lastPlayers.Count <= 5)
            {
                int answer = _ui.LoadLastPlayers();
                if (answer == 1)
                {
                    foreach (string name in lastPlayers)
                    {
                        decimal chips = _storage.LoadChips(name); // Loads saved chips.

                        _players.Add(new Player(name, AskMoveWithDealer, chips));
                    }

                    _ui.ShowMessage("Tidigare spelare har återställts!");
                }
            }
            List<string> savedNames = _storage.LoadPlayerNames();  // Load previously saved player names.
            if (savedNames.Count > 0)  // Check if any previous players exist.
            {
                _ui.ShowMessage("Tidigare spelare:");// Show the names of previously saved players.

                foreach (string name in savedNames)
                {
                    _ui.ShowMessage(name);
                }
            }
            int count = _ui.AskPlayerCount();
            for (int i = 0; i < count; i++)
            {
            // Let the user choose between a saved player and a new player.
                string name;

                if (savedNames.Count > 0) // Check if saved players exist.
                {
                    int choice = _ui.CheckSavedPlayers();
                    if (choice == 1) // Select a previously saved player.
                    {
                        name = _ui.AskName();

                        while (!savedNames.Contains(name)) // Check if the player exists.
                        {
                            _ui.ShowMessage("Spelaren finns inte. Försök igen:");
                            name = _ui.AskName();
                        }
                    }
                    else // Create a new player.
                    {
                        name = _ui.AskName();
                    }
                }
                else // Ask for a name if no players are saved.
                {
                    name = _ui.AskName();
                }
                // Prevent duplicate player names in the same game.
                while (_players.Exists(p => p.Name == name))
                {
                    _ui.ShowMessage("Namnet används redan. Ange ett annat namn:");
                    name = _ui.AskName();
                }
                if (savedNames.Contains(name)) // Check if the player has played before.
                {
                    _ui.ShowMessage($"Välkommen tillbaka, {name}!");
                }
                decimal chips = _storage.LoadChips(name); // Loads saved chips.

                _players.Add(new Player(name, AskMoveWithDealer, chips)); // Adds a human player.
            }
            int freeSeats = 5 - count; //  Max 5 players in total

            if (freeSeats > 0) //Only ask if there is room left
            {
                int computerCount = _ui.AskComputerPlayerCount(freeSeats);//Number of free seats is shown in the question to the player

                for (int i = 0; i < computerCount; i++)
                {
                    // Random.Shared.Next(2) gives 0 or 1 to pick one of the two strategies
                    IPlayerStrategy strategy = Random.Shared.Next(2) == 0
                        ? new SimpleStrategy()
                        : new BasicStrategy();

                    _players.Add(
                        new ComputerPlayer($"Dator {i + 1}", strategy, _dealer)); //A new computer player is added with a name and a strategy and the dealer (to be able to see the open cards)
                }
            }
            // Save the human players for the next game.
            List<string> playerNames = new();

            foreach (Player player in _players)
            {
                if (player is not ComputerPlayer)
                {
                    playerNames.Add(player.Name);
                }
            }

            _storage.SavePlayers(playerNames); // Saves the names in players.txt.
        }
        

        _players.RemoveAll(player => player.Betting.Chips == 0); // Removes players without chips.

        if (_players.Count == 0)
        {
            _ui.ShowMessage("Alla spelare har slut på marker.");
            return;
        }

        foreach (Player player in _players) // Handles bets before the round.
        {
            decimal amount;

            if (player is ComputerPlayer) // Computer players bet automatically.
            {
                amount = Math.Min(10m, player.Betting.Chips); // Bets 10 chips or the remaining balance.

                _ui.ShowMessage(
                    $"{player.Name} satsar automatiskt {amount} marker."
                );
            }
            else // Human players choose their own bet.
            {
                amount = _ui.AskBet(player);
            }

            bool accepted = player.Betting.PlaceBet(amount); // Registers the bet.

            if (!accepted)
            {
                _ui.ShowMessage("Ogiltig satsning.");
                return;
            }
        }

        for (int i = 0; i < _players.Count; i++) //Need to clean all hands before starting the next round
        {
            _players[i].Hand.Clear();

            if (_players[i] is ComputerPlayer bot) // Only computer players have a strategy
            {
                IPlayerStrategy newStrategy = Random.Shared.Next(2) == 0
                    ? new SimpleStrategy()
                    : new BasicStrategy();

                bot.SetStrategy(newStrategy); // Swap strategy while the game is running
            }
        }

        _dealer.Hand.Clear();
        _dealer.HideHoleCard(); // Hides the dealer's second card.

        if (_deck.IsCutCardReached) // Checks if the shoe needs shuffling.
        {
            _ui.ShowMessage(
                "\n[INFO] Skiljekortet har nåtts! Skon blandas om inför nästa runda..."
            );

            _deck.ShuffleShoe();
        }

        List<Outcome> results = _round.PlayRound(
            _deck, _players, _dealer
        ); // Plays the round and returns the results. It will run Round class

        for (int i = 0; i < _players.Count; i++)    //It shows everyone hands before leaving the round
        {
            _ui.ShowHand(_players[i], false);
        }

        _ui.ShowHand(_dealer, false); // Shows all dealer cards.

        for (int i = 0; i < _players.Count; i++)  //Shows the outcome of every player.
        {
            Player player = _players[i];

            _ui.ShowMessage($"{player.Name}");
            _ui.ShowOutcome(results[i]); // Shows the player's result.

            player.Betting.SettleBet(results[i]); // Pays out based on the result.

            _storage.SaveChips(
                player.Name,
                player.Betting.Chips
            ); // Saves the player's remaining chips.

            _ui.ShowMessage(
                $"Marker kvar: {player.Betting.Chips}"
            );

            _storage.SaveResult(player.Name, results[i]);  //It will save all data from every player in the end of the round
        }

        for (int i = _players.Count - 1; i >= 0; i--) // Removes players with no chips left.
        {
            if (_players[i].Betting.Chips == 0)
            {
                _ui.ShowMessage(
                    $"{_players[i].Name} har slut på marker och lämnar bordet."
                );

                _players.RemoveAt(i);
            }
        }
    }

    private Move AskMoveWithDealer(Participant participant)
    {
        // ---------- CARD COUNTING DISPLAY ----------
        // Print the Running Count and True Count
        _ui.ShowMessage(
            $"[KORTRÄKNING] Löpande: {_deck.RunningCount} | Sann: {_deck.GetTrueCount()}");
        // Print the humoristic indicator message telling the player  if the deck  is favorabel or not
        _ui.ShowMessage(
            $"[STATUS] {_deck.GetAdvantageIndicator()}\n"
        ); // Shows the card counting status.

        // ---------- NEW CARD DISPLAY ----------
        // Check if the player aready drew a card  this round
        if (participant.Hand.Cards.Count > 2)
        {
            // Prints the last card drawn using [^1]
            _ui.ShowMessage("Ditt nya kort:");
            _ui.ShowMessage($"{participant.Hand.Cards[^1]}");
        }

        // ---------- CURRENT HAND DISPLAY ----------
        // Prints the player's name,cards and total score
        _ui.ShowMessage($"{participant.Name}s hand (Totalt: {participant.Hand.Value})");
        // Ask the player to HIT or STAND and return their choice.
        return _ui.AskMove(
            participant,
            _dealer,
            _players
        ); // Returns Hit or Stand.
    }

    private void ShowResults() //This method will print to user the saved data
    {
        List<string> savedResults = _storage.LoadResults();

        if (savedResults.Count == 0)  //If there isn't any data yet, send a feedback
        {
            _ui.ShowMessage("Inga resultat sparade ännu.");
        }
        else    //just show to user all saved data as a message
        {
            for (int i = 0; i < savedResults.Count; i++)
            {
                _ui.ShowMessage(savedResults[i]);
            }

            Dictionary<string, int[]> stats = _storage.LoadPlayerStats();  // Load the total wins, losses and draws for each player.


            _ui.ShowMessage("\n--- Spelarstatistik ---"); // Show the statistics heading.

            foreach (var player in stats)// Go through each player and display their statistics.
            {
                _ui.ShowMessage($"{player.Key}: Vinster: {player.Value[0]}, Förluster: {player.Value[1]}, Oavgjorda: {player.Value[2]}");
            }
        }

    }
}
