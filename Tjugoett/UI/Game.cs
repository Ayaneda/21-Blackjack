
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
                    AddPlayers();    //This will add player between rounds.
                    break;

                case 3:
                    RemovePlayer();   //Remove a player
                    break;
                case 4:
                    ShowResults();   //Show all save data 
                    break;
                case 5:
                    isRunning = false;
                    break;
            }
        }
    }

    private void PlayRound()
    {
        _players.RemoveAll(player => player.Betting.Chips == 0); // Removes players without chips.

        if (_players.Count == 0)
        {
            ConsoleUi.ShowMessage("Det finns inte spelare vid bordet. Lägga spelare först.");
            return;
        }

        foreach (Player player in _players) // Handles bets before the round.
        {
            decimal amount;

            if (player is ComputerPlayer) // Computer players bet automatically.
            {
                amount = Math.Min(10m, player.Betting.Chips); // Bets 10 chips or the remaining balance.

                ConsoleUi.ShowMessage(
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
                ConsoleUi.ShowMessage("Ogiltig satsning.");
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
            ConsoleUi.ShowMessage("\n[INFO] Skiljekortet har nåtts! Skon blandas om inför näasta runda....");
            _deck.ShuffleShoe();
        }

        List<Outcome> results = _round.PlayRound(
            _deck, _players, _dealer
        ); // Plays the round and returns the results.

        for (int i = 0; i < _players.Count; i++)    //It shows everyone hands before leaving the round
        {
            ConsoleUi.ShowHand(_players[i], false);
        }

        ConsoleUi.ShowHand(_dealer, false);

        for (int i = 0; i < _players.Count; i++)  //Shows the outcome of every player.
        {
            Player player = _players[i];

            ConsoleUi.ShowMessage($"{player.Name}");
            if (player is ComputerPlayer bot) // only computer players have a strategy
            {
                ConsoleUi.ShowMessage($"Strategi: {bot.StrategyName}");
            }
            _ui.ShowOutcome(results[i]); // Shows the player's result.

            player.Betting.SettleBet(results[i]); // Pays out based on the result.

            _storage.SaveChips(
                player.Name,
                player.Betting.Chips
            ); // Saves the player's remaining chips.

            ConsoleUi.ShowMessage(
                $"Marker kvar: {player.Betting.Chips}"
            );

            _storage.SaveResult(player.Name, results[i]);  //It will save all data from every player in the end of the round
        }

        for (int i = _players.Count - 1; i >= 0; i--) // Removes players with no chips left.
        {
            if (_players[i].Betting.Chips == 0)
            {
                ConsoleUi.ShowMessage(
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
        ConsoleUi.ShowMessage($"[KORTRÄKNING] Löpande: {_deck.RunningCount} | Sann: {_deck.GetTrueCount()}");
        // Print the humoristic indicator message telling the player  if the deck  is favorabel or not
        ConsoleUi.ShowMessage($"[STATUS] {_deck.GetAdvantageIndicator()}\n");
        
        //-----------NEW CARD DISPLAY-----------------
        // Check if the player aready drew a card  this round
        if (participant.Hand.Cards.Count > 2)
        {
            // Prints the last card drawn using [^1]
            ConsoleUi.ShowMessage($"Ditt nya kort:");
            ConsoleUi.ShowMessage($"{participant.Hand.Cards[^1]}");
        }

        // ---------- CURRENT HAND DISPLAY ----------
        // Prints the player's name and total score
        ConsoleUi.ShowMessage($"{participant.Name}s hand:   (Totalt: {participant.Hand.Value})");

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
            ConsoleUi.ShowMessage("Inga resultat sparade ännu.");
        }
        else    //just show to user all saved data as a message
        {
            for (int i = 0; i < savedResults.Count; i++)
            {
                ConsoleUi.ShowMessage(savedResults[i]);
            }
        }
    }
    private void AddPlayers()
    {
        if (_players.Count < 5 && _players.Count >= 0)   //Check if there are player spots.
        {
            int playerType =_ui.AskTypeOfPlayer();
            switch (playerType)
            {
                case 1:
                    int count = _ui.AskPlayerCount(_players);

                    for (int i = 0; i < count; i++)
                    {
                        string name = _ui.AskName();
                        decimal chips = _storage.LoadChips(name); // Loads saved chips.

                        _players.Add(new Player(name, AskMoveWithDealer, chips)); // Adds a human player.
                    }
                    break;
                
                case 2:
                    int freeSeats = 5 - _players.Count; //  Max 5 players in total

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
                    break;
            }
        }
        else
        {
            ConsoleUi.ShowMessage("Det finns inga lediga platser.");
        }
    }
    private void RemovePlayer()
    {
        foreach(Player player in _players)
        {
            ConsoleUi.ShowMessage($"{player}");
        }
        Player playerToRemove = _ui.AskNameToRemove(_players);
        _players.Remove(playerToRemove);
    }
}
