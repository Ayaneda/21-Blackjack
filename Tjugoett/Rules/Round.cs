using System.Security.Cryptography.X509Certificates;
using Tjugoett.Cards;
using Tjugoett.Participants;

namespace Tjugoett.Rules;

public class Round
{
    public void SpelaRunda(Deck deck, Player p1, Player p2, Dealer dealer)
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
        if (p2.Hand.Value < 21)
        {
            while (true)
            {
                Move choice = p2.GetMove();

                if (choice == Move.Hit)
                {
                    deck.DealCards(p2);
                }
                if (p2.Hand.Value > 21)
                {
                    break;
                }
                if (choice == Move.Stand)
                {
                    break;
                }
            }
        }
    }

}
