namespace Tjugoett.Cards;

public class Hand
{
    private List<Card> cards = new List<Card>(); // Keep the card list private so Hand controls how cards are added.
    public void AddCard(Card card)
    {
        cards.Add(card);
    }
    public int GetValue()
    {
        int total = 0;
        int aceCount = 0;
        foreach (Card card in cards) // Add the value of each card in the hand.
        {
            total += card.Value;

            if (card.CardRank == Rank.Ace)
            {
                aceCount++;
            }
        }

        while (total > 21 && aceCount > 0) // Change Ace from 11 to 1 if the total is over 21.
        {
            total -= 10;
            aceCount--;
        }
        return total;
    }
}

