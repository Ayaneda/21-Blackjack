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

    // Constructor initialize the shoe snd fills it with  6 decks
    public Shoe(int numberOfDeck)
    {
        // We loop through the number of decks we want to add
        for (int i = 0; i < numberOfDeck; i++)
        {
            // We create a temporary deck 
            Deck tempDeck = new Deck();
            
            // We empty the temporary deck an move all cards to our main shoe
            while (tempDeck.CardLeft() > 0)
            {
                Card card = tempDeck.DealCard();
                _cards.Add(card); // Add the card to the shoe list
            }
        }

        // When all decks are moves, we shuffle all decks
        ShuffleShoe();
        
    }

    // A simpel, classic Fisher-Yates shuffle
    public void ShuffleShoe()
    {
        Random random = new Random(); // Random generator
        int n = _cards.Count; // Counter (312)

        while (n > 1) // Last card to 0
        {
            n--;
            int k = random.Next(n + 1); // A random position
            Card value = cards[k];
            _cards[k] = _cards[n];
            _cards[n]; = value;
        }

        // The shoe has just been shuffled, so the divider card has not been reached yet.
        IsCutCardReached = false;

    }
    
    // Same method name as in Deck. So I dont need to change it in Deck.cs
    public Card DealCard()
    {
        // Take the top card from the list.
        Card cardToDeal = _cards[0];
        _cards.RemoveAt(0);
    

        return cardToDeal;
    }

}

