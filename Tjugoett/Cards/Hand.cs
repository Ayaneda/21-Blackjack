namespace Tjugoett.Cards;

public class Hand
{
    private List<Card> cards = new List<Card>();
    public void AddCard(Card card)
    {
        cards.Add(card);
    }
}

