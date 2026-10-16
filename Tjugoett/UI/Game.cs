namespace Tjugoett.UI;

using System.Runtime.CompilerServices;
using Tjugoett.Cards;
using Tjugoett.Participants;
using Tjugoett.Rules;
public class Game
{
    private Deck _deck;
    private List<Player> _players;
    private Dealer _dealer;
    private ConsoleUi _ui;
    public Game(Deck deck, List<Player> players, Dealer dealer, ConsoleUi ui)
    {
        _deck = deck;
        _players = players;
        _dealer = dealer;
        _ui = ui;
    }

    public void Run()
    {
        bool isRunning = true;
        
        
        while (isRunning)
        {
            int choice = _ui.ShowMenu();
            switch (choice)
            {
                case 1:
                    PlayRound();
                    break;
                case 2:
                    _ui.ShowMessage("Inga resultad ännu.");
                    break;
                case 3:
                    isRunning = false;
                    break;

            }

        }
    }
}
