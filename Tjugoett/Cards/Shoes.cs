using Tjugoett.Cards;
using Tjugoett.Participants;

namespace Tjugoett.Cards;

public class Shoes
{
    // A singel list containing all cards from all decks combinded.
    private List<Card> _cards = new List <Card>();

    private int _cutCardPosition; // How many cards are left when the cut card is reached

    private int _numberOfDecks;

    // A boolean variable that set "true" wwhen passed divider card
    public bool IsCutCardReached { get; private set; }

    public int RunningCount { get; private set; } // Running count

    // Constructor initialize the shoe snd fills it with  6 decks
    public Shoes(int numberOfDeck = 6)
    {
        // Save how many decks to use
        _numberOfDecks = numberOfDeck;
        // Fills and shuffle the shoe for the first time
        ShuffleShoe();
        
    }

    // A simpel, classic Fisher-Yates shuffle
    public void ShuffleShoe()
    {
        // Empty the cards that are left
        _cards.Clear();

        for (int i = 0; i < _numberOfDecks; i++)
        {
            // Refills the shoe with brand new decks
            Deck tempDeck = new Deck();
            foreach (Card card in tempDeck.deck)
            {
                _cards.Add(card);
            }
        }


        Random random = new Random(); // Random generator
        int n = _cards.Count; // Counter (312)

        while (n > 1) // Last card to 0
        {
            n--;
            int k = random.Next(n + 1); // A random position
            Card value = _cards[k];
            _cards[k] = _cards[n];
            _cards[n] = value;
        }

        // The shoe has just been shuffled, so the divider card has not been reached yet.
        IsCutCardReached = false;

        // Place the cut card at a random position in the back quarter of the shoe
        _cutCardPosition = random.Next(1, _cards.Count / 4 + 1);  

        RunningCount = 0;

    }
    
    // Same method name as in Deck. So I dont need to change it in Deck.cs
    public Card DealCard(bool isHidden = false)
    {
        // If the shoe runs empty during the run , it refills automatically
        if (_cards.Count == 0)
        {
            ShuffleShoe();
        }


        // Take the top card from the list.
        Card cardToDeal = _cards[0];
        _cards.RemoveAt(0); // Remove the card so it isn't use again.

        // Count the card that just drawn according to the Hi-Lo system
        if (!isHidden)
        {
            UppdateHiLoCount(cardToDeal);
        }

        // The cut card is reached when the cards left reach the random cut card position
        if (_cards.Count <= _cutCardPosition)
        {
           IsCutCardReached = true;
        }

        return cardToDeal;
    
    }

    // A Method that checks the card value and updates RunningCount.
    public void UppdateHiLoCount(Card card)
    {
        // Check the card's value/Rank
        // If your project uses an int or an Enum for rank, adapt it here
        int rankValue = (int)card.CardRank;

        // Card 2 to 6 givs +1
        if (rankValue >= 2 && rankValue <= 6)
        {
            RunningCount++;
        }
        // Card 10, Jack, Queen, King, Ace givs -1
        // Here we assume that 11=Jack, 12=Queen, 13=King, 14=Ace or similar in your Enum
        else if (rankValue >= 10) 
        {
            RunningCount--;
        }
        // 7-9 ger 0 så då satasar man inte!!
    }


    public double GetTrueCount()
    {
        // Calcilate how manny Decks of 52 cards is remaining in the shoe
        double decksLeft =  (double)_cards.Count / 52;

        // Avoid division with Zero
        if (decksLeft < 0.5) return RunningCount; 

        // Calculate True Count: Running count divided by remaining decks
        return Math.Round(RunningCount / decksLeft, 1);
    }


    public string GetAdvantageIndicator()
    {
        double truecount = GetTrueCount();

        if (truecount >= 3)return "Money money money! (Många höga kort kvar) ";
        if (truecount > 0)return "Helt ok! (Våga satsa) ";
        if (truecount == 0)return "Ja ! Ta en chans! ";
        return "Do you feel LUCKY, PUNK! (Många småkort kvar)";
    }
    
    public int CardLeft()
    {
        return _cards.Count;
    }
    


}

