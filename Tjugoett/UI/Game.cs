using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
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
    public Game(Deck deck, List<Player> players, Dealer dealer, ConsoleUi ui, Round round)
    {
        _deck = deck;
        _players = players;
        _dealer = dealer;
        _ui = ui;
        _round = round;

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
    private void PlayRound()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].Hand.Clear();
        }
        _dealer.Hand.Clear();
        _deck.CardLeft();

        List<Outcome> results = _round.PlayRound(_deck, _players, _dealer);

        for (int i = 0; i < _players.Count; i++)
        {
            _ui.ShowHand(_players[i], false);
        }

        _ui.ShowHand(_dealer, false);

        for (int i = 0; i < _players.Count; i++)
        {
            _ui.ShowMessage($"{_players[i].Name}");
            _ui.ShowOutcome(results[i]);
        }
    }
}
