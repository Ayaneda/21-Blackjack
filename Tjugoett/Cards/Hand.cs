namespace Tjugoett.Cards;

public class Hand
{
    private List<Card> cards = new List<Card>();
    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    // Waiting for Card to expose Rank and Suit before adding hand value logic.
}

