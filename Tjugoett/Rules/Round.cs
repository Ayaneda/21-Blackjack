using Tjugoett.Cards;
using Tjugoett.Participants;

namespace Tjugoett.Rules;

public class Round
{
    public Outcome PlayRound(Deck deck, Player p1, Dealer dealer)
    {
        deck.DealCards(p1);
        deck.DealCards(dealer);
    

        if (p1.Hand.Value < 21)
        {
            while (true)
            {
                Move choice = p1.GetMove();

                if (choice == Move.Hit)
                {
                    deck.DealCard(p1);
                }
                else if (p1.Hand.Value > 21)
                {
                    break;
                }
                else if (choice == Move.Stand)
                {
                    break;
                }
            }
        }
        while (true)
        {
            if (dealer.Hand.Value <= 16)
            {
                deck.DealCard(dealer);
            }
            else if (dealer.Hand.Value >= 17)
            {
                break;
            }
            else
            {
                break;
            }
        }
    
        if (p1.Hand.Value > 21)
        {
            return Outcome.Lose;
        }
        else if (dealer.Hand.Value > 21)
        {
            return Outcome.Win;
        }
        else if (p1.Hand.Value > dealer.Hand.Value)
        {
            return Outcome.Win;
        }
        else if (p1.Hand.Value == dealer.Hand.Value)
        {
            return Outcome.Push;
        }
        else
        {
            return Outcome.Lose;
        }
    }

}
