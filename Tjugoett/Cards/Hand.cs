using System.Data;

namespace Tjugoett.Cards;

public class Hand
{
    private readonly List<Card> handCards = new List<Card>(); // Keep the card list private so Hand controls how cards are added.
    public void AddCard(Card card)
    {
        handCards.Add(card);
    }
    public void Clear()  // Remove all cards from the hand before a new round.
    {
        handCards.Clear();
    }
    public int Value
    {
        get
        {
            int total = 0;
            int aceCount = 0;
            foreach (Card card in handCards) // Add the value of each card in the hand.
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
    public bool IsBust // Returns true if the hand value is over 21.
    {
        get
        {
            return Value > 21;
        }
    }

    public bool IsTwentyOne // Returns true if the hand value is exactly 21.
    {
        get
        {
            return Value == 21;
        }
    }

    public bool IsSoft // Returns true if an Ace is still counted as 11.
    {
        get
        {
            int total = 0;
            int aceCount = 0;
            foreach (Card card in handCards)
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
            return aceCount > 0;
        }
    }
    public void ShowHand()
    {
        foreach(Card card in handCards)
        {
            Console.Write($"{card}");
        }
        Console.WriteLine("");
    }
}
