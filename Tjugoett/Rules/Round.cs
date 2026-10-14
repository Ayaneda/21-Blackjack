using Tjugoett.Cards;
using Tjugoett.Participants;

namespace Tjugoett.Rules;

public class Round
{
    public void PlayRound(Deck deck, Player p1, Dealer dealer)
    {
        deck.DealCards(p1);
        deck.DealCards(p2);
        deck.DealCards(dealer);
    

        if (p1.Hand.Value < 21)
        {
            while (true)
            {
                Move choice = p1.GetMove();

                if (choice == Move.Hit)
                {
                    deck.DealCards(p1);
                }
                if (p1.Hand.Value > 21)
                {
                    break;
                }
                if (choice == Move.Stand)
                {
                    break;
                }
            }
        }
        if (dealer.Hand.Value < 17)
        {
            while (true)
            {
                deck.DealCards(dealer);
            }
        }
    }

}
