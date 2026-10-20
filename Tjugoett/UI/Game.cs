using Tjugoett.Cards;
using Tjugoett.Participants;
using Tjugoett.Rules;
using Tjugoett.Storage;

namespace Tjugoett.UI;

public class Game
{
    private Deck _deck;
    private List<Player> _players;
    private Dealer _dealer;
    private ConsoleUi _ui;
    private Round _round;
    private GameStorage _storage;
    public Game()   //This is the contructor for the class Game
    {
        _deck = new Deck();
        _players = new List<Player>();
        _dealer = new Dealer("Dealer");
        _ui = new ConsoleUi();
        _round = new Round();
        _storage = new GameStorage();
    }

    public void Run()   //this Will run the game
    {
        bool isRunning = true;  //a bool to stop the game when is change


        while (isRunning)
        {
            int choice = _ui.ShowMenu();
            switch (choice)
            {
                case 1:
                    PlayRound();    //This will run the method for
                    break;
                case 2:
                    ShowResults();   //Show all save data 
                    break;
                case 3:
                    isRunning = false;
                    break;

            }

        }
    }
    private void PlayRound()
    {
        if (_players.Count == 0)   //The first time will ask for players because there is not player yet
        {
            int count = _ui.AskPlayerCount();
            for (int i = 0; i < count; i++)
            {
                string name = _ui.AskName();
                int chips = _storage.LoadChips(name); // Load saved chips.
                _players.Add(new Player(name, AskMoveWithDealer, chips)); // Create player with saved chips.
            }
        }
        _players.RemoveAll(player => player.Betting.Chips == 0);// Remove plyayers with no chips left.
        if (_players.Count == 0)
        {
            _ui.ShowMessage("Alla spelare har slut på marker.");
            return;
        }
        foreach (Player player in _players) // Ask players to bet before dealing cards.
        {
            int amount = _ui.AskBet(player);

            bool accepted = player.Betting.PlaceBet(amount); // Try to place the bet.

            if (!accepted) // Check if the bet is invalid.
            {
                _ui.ShowMessage("Ogiltig satsning.");
                return;
            }
        }
        for (int i = 0; i < _players.Count; i++)    //Need to clean all hands before starting the next round
        {
            _players[i].Hand.Clear();
        }
        _dealer.Hand.Clear();
        _dealer.HideHoleCard();
        _deck.CardLeft();       //It will check if there are enough cards for the next round

        List<Outcome> results = _round.PlayRound(_deck, _players, _dealer);   //It will run Round class

        for (int i = 0; i < _players.Count; i++)     //It shows everyone hands before leaving the round
        {
            _ui.ShowHand(_players[i], false);
        }

        _ui.ShowHand(_dealer, false);

        for (int i = 0; i < _players.Count; i++)   //Shows the outcome of every player.
        {
            Player player = _players[i];
            _ui.ShowMessage($"{player.Name}");
            _ui.ShowOutcome(results[i]);
            player.Betting.SettleBet(results[i]); // Update chips after the round.
            _storage.SaveChips(player.Name, player.Betting.Chips); // Save remaining chips.
            _ui.ShowMessage($"Marker kvar: {player.Betting.Chips}"); // Show remaining chips.
            _storage.SaveResult(_players[i].Name, results[i]);   // Save the game result.
        }
    }

    private Move AskMoveWithDealer(Participant participant) // Player sends itself to this method. It adds the dealer and asks ConsoleUi for Hit or Stand
    {
        return _ui.AskMove(participant, _dealer);
    }

    private void ShowResults()  //This method will print to user the saved data
    {
        List<string> savedResults = _storage.LoadResults();

        if (savedResults.Count == 0)    //If there isn't any data yet, send a feedback
        {
            _ui.ShowMessage("Inga resultat sparade ännu.");
        }
        else                    //just show to user all saved data as a message
        {
            for (int i = 0; i < savedResults.Count; i++)
            {
                _ui.ShowMessage(savedResults[i]);
            }
        }
    }

}
