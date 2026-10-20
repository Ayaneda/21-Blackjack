using System.ComponentModel;
using System.Runtime.InteropServices;
using Tjugoett.Cards;

namespace Tjugoett.Shoes;

public class Shoes
{
    // A singel list containing all cards from all decks combinded.
    private List<Card> _cards = new List <Card>();

    // A boolean variable that set "true" wwhen passed divider card
    public bool IsCutCardReached { get; private set; }

    public Shoe(int numberOfDeck)
    {
        for (int i = 0; i < numberOfDeck; i++)
        {
            Deck tempDeck = new Deck();
        }
    }
    

}

