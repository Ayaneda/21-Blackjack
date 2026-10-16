using Tjugoett.Cards;
using Tjugoett.Participants;
using Tjugoett.Rules;

namespace Tjugoett.UI;

public class Game
{
    private Deck _deck;
    private List<Player> _players;
    private Dealer _dealer;
    private ConsoleUi _ui;
    private Round _round;
    public Game()   //This is the contructor for the class Game
    {
        _deck = new Deck();
        _players = new List<Player>();
        _dealer = new Dealer("Dealer");
        _ui = new ConsoleUi();
        _round = new Round();

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
                    _ui.ShowMessage("Inga resultat ännu.");   //I will need a method to get save the information    !!!WAIT FOR ADD!!!
                    break;
                case 3:
                    isRunning = false;
                    break;

            }

        }
    }
    private void PlayRound()   
    {
        if(_players.Count == 0)   //The first time will ask for players because there is not player yet
        {
            int count = _ui.AskPlayerCount();
            for (int i = 0; i < count; i++)
            {
                string name = _ui.AskName();
                _players.Add(new Player(name, _ui.AskMove));
            }
        }
        for (int i = 0; i < _players.Count; i++)    //Need to clean all hands before starting the next round
        {
            _players[i].Hand.Clear();
        }
        _dealer.Hand.Clear();
        _deck.CardLeft();       //It will check if there are enough cards for the next round

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
        }
    }
}
