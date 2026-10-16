namespace Tjugoett.UI;
using Tjugoett.Cards;
using Tjugoett.Participants;
using Tjugoett.Rules;
public class Game
{
    private Deck _deck;
    private List<Player> _players;
    private Dealer _dealer;
    public Game(Deck deck, List<Player> players, Dealer dealer)
    {
        _deck = deck;
        _players = players;
        _dealer = dealer;
    }

    
}
