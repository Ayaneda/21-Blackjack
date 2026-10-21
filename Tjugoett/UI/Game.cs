
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

    public Game() // Constructor for the Game class.
    {
        _deck = new Shoes(6); // Creates a shoe with six decks.
        _players = new List<Player>();
        _dealer = new Dealer("Dealer");
        _ui = new ConsoleUi();
        _round = new Round();
        _storage = new GameStorage();
    }

    public void Run() // Runs the main game menu.
    {
        bool isRunning = true;

        while (isRunning)
        {
            int choice = _ui.ShowMenu();

            switch (choice)
            {
                case 1:
                    PlayRound(); // Starts a game round.
                    break;

                case 2:
                    ShowResults(); // Shows saved results.
                    break;

                case 3:
                    isRunning = false; // Exits the game.
                    break;
            }
        }
    }

    private void PlayRound()
    {
        if (_players.Count == 0) // Creates players when the table is empty.
        {
            int count = _ui.AskPlayerCount();

            for (int i = 0; i < count; i++)
            {
                string name = _ui.AskName();
                decimal chips = _storage.LoadChips(name); // Loads saved chips.

                _players.Add(new Player(name, AskMoveWithDealer, chips)); // Adds a human player.
            }

            int freeSeats = 5 - count; // Calculates remaining seats.

            if (freeSeats > 0)
            {
                int computerCount = _ui.AskComputerPlayerCount(freeSeats);

                for (int i = 0; i < computerCount; i++)
                {
                    IPlayerStrategy strategy = Random.Shared.Next(2) == 0
                        ? new SimpleStrategy()
                        : new BasicStrategy();

                    _players.Add(
                        new ComputerPlayer($"Dator {i + 1}", strategy, _dealer)
                    ); // Adds a computer player.
                }
            }
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

        for (int i = 0; i < _players.Count; i++) // Clears hands before a new round.
        {
            _players[i].Hand.Clear();

            if (_players[i] is ComputerPlayer bot) // Updates computer player strategies.
            {
                IPlayerStrategy newStrategy = Random.Shared.Next(2) == 0
                    ? new SimpleStrategy()
                    : new BasicStrategy();

                bot.SetStrategy(newStrategy); // Changes the computer player's strategy.
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
        ); // Plays the round and returns the results.

        for (int i = 0; i < _players.Count; i++) // Shows every player's final hand.
        {
            _ui.ShowHand(_players[i], false);
        }

        _ui.ShowHand(_dealer, false); // Shows all dealer cards.

        for (int i = 0; i < _players.Count; i++) // Handles results and payouts.
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

            _storage.SaveResult(
                player.Name,
                results[i]
            ); // Saves the game result.
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

        _ui.ShowMessage(
            $"[KORTRÄKNING] Löpande: {_deck.RunningCount} | Sann: {_deck.GetTrueCount()}"
        ); // Shows the running count and true count.

        _ui.ShowMessage(
            $"[STATUS] {_deck.GetAdvantageIndicator()}\n"
        ); // Shows the card counting status.

        // ---------- NEW CARD DISPLAY ----------

        if (participant.Hand.Cards.Count > 2) // Checks if the player has drawn an additional card.
        {
            _ui.ShowMessage("Ditt nya kort:");
            _ui.ShowMessage($"{participant.Hand.Cards[^1]}"); // Shows the most recently drawn card.
        }

        // ---------- CURRENT HAND DISPLAY ----------

        _ui.ShowMessage(
            $"{participant.Name}s hand (Totalt: {participant.Hand.Value})"
        ); // Shows the player's name and current score.

        return _ui.AskMove(
            participant,
            _dealer,
            _players
        ); // Returns Hit or Stand.
    }

    private void ShowResults() // Displays previously saved results.
    {
        List<string> savedResults = _storage.LoadResults();

        if (savedResults.Count == 0)
        {
            _ui.ShowMessage("Inga resultat sparade ännu.");
        }
        else
        {
            for (int i = 0; i < savedResults.Count; i++)
            {
                _ui.ShowMessage(savedResults[i]); // Prints each saved result.
            }
        }
    }
}
