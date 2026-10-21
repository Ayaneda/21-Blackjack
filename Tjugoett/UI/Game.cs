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
    public Game()   //This is the contructor for the class Game
    {
        _deck = new Shoes(6);
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
                _players.Add(new Player(name, AskMoveWithDealer));
            }

            int freeSeats = 5 - count; // Max 5 players in total
            if (freeSeats > 0)         // Only ask if there is room left
            {
                int computerCount = _ui.AskComputerPlayerCount(freeSeats); //Number of free seats is shown in the question to the player
                for (int i = 0; i < computerCount; i++)
                {
                    // Random.Shared.Next(2) gives 0 or 1 to pick one of the two strategies
                    IPlayerStrategy strategy = Random.Shared.Next(2) == 0 ? new SimpleStrategy() : new BasicStrategy();
                    _players.Add(new ComputerPlayer($"Dator {i + 1}", strategy, _dealer)); //A new computer player is added with a name and a strategy and the dealer (to be able to see the open cards)
                }
            }
        }
        for (int i = 0; i < _players.Count; i++)    //Need to clean all hands before starting the next round
        {
            _players[i].Hand.Clear();

            if (_players[i] is ComputerPlayer bot) // Only computer players have a strategy
            {
                IPlayerStrategy newStrategy = Random.Shared.Next(2) == 0 ? new SimpleStrategy() : new BasicStrategy();
                bot.SetStrategy(newStrategy); // Swap strategy while the game is running
            }
        }
        _dealer.Hand.Clear();
        _dealer.HideHoleCard();


        if (_deck.IsCutCardReached)
        {
            _ui.ShowMessage("\n[INFO] Skiljekortet har nåtts! Skon blandas om inför näasta runda....");
            _deck.ShuffleShoe();
        }
    

        List<Outcome> results = _round.PlayRound(_deck, _players, _dealer);   //It will run Round class

        for (int i = 0; i < _players.Count; i++)     //It shows everyone hands before leaving the round
        {
            _ui.ShowHand(_players[i], false);
        }

        _ui.ShowHand(_dealer, false);

        for (int i = 0; i < _players.Count; i++)   //Shows the outcome of every player.
        {
            _ui.ShowMessage($"{_players[i].Name}");
            _ui.ShowOutcome(results[i]);
            _storage.SaveResult(_players[i].Name, results[i]);   //It will save all data from every player in the end of the round
        }
    }

    private Move AskMoveWithDealer(Participant participant) 
    {
        // ----------CARD COUNTING DISPLAY------------
        // Print the Running Count and True Count
        _ui.ShowMessage($"[KORTRÄKNING] Löpande: {_deck.RunningCount} | Sann: {_deck.GetTrueCount()}");
        // Print the humoristic indicator message telling the player  if the deck  is favorabel or not
        _ui.ShowMessage($"[STATUS] {_deck.GetAdvantageIndicator()}\n");
        
        //-----------NEW CARD DISPLAY-----------------
        // Check if the player aready drew a card  this round
        if (participant.Hand.Cards.Count > 2)
        {    
            // Prints the last card drawn using [^1]
            _ui.ShowMessage($"Ditt nya kort: {participant.Hand.Cards[^1]}");
        }
         
        //-----------CURRENT HAND DISPLAY-------------
        // Prints the player's name,cards and total score
        _ui.ShowMessage($"{participant.Name}s hand: {participant.Hand} (Totalt: {participant.Hand.Value})");
        // Ask the player to HIT or STAND and return their choice.
        return _ui.AskMove(participant, _dealer, _players);
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
