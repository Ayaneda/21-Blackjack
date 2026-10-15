using Tjugoett.Cards;
using Tjugoett.Participants;

namespace Tjugoett.Rules;

public class Round
{
    // Here we recive three objects from the game Loop, deck , p1 and dealer
    public List<Outcome> PlayRound(Deck deck, List<Player> players, Dealer dealer)
    {
        List<Outcome> roundResults = new List<Outcome>();
        // We give two cards to the player and to the dealer
        //deck.DealCards(p1);
        //deck.DealCards(dealer);

        for (int i =0; i < players.Count; i++)
        {
            deck.DealCards(players[i]);
        }
        deck.DealCards(dealer);

        for (int i = 0; i < players.Count; i++)
        {
            
            // Here we control if player has a Blacjack
            if (players[i].Hand.Value == 21)
            {
                continue;
            }
        
            // if the cards are less than 21 you can get another card
            if (p1.Hand.Value < 21)
            {   
                // Here, the player is kept in a loop until that the player gets the right value
                // that the player gets the right value
                while (true)
                {
                    Move choice = p1.GetMove(); // Ask the playern if the palern wants to Hit or Stand.

                    if (choice == Move.Hit) // The playern get another card
                    {
                        deck.DealCard(p1);
                    } 
                    else if (choice == Move.Stand) // The player choose Stand and the loop breaks
                    {
                        break;
                    }
            
                    if (players[i].Hand.Value > 21) // Here we control the playerns choise
                    {                               // if the playern took a card and the value is greater then 21
                        break;                      // then the player is bust
                    }
        
                }
           
            }

        }
            while (true)
            {   
                // The dealer has no options, as they must strictly follow the set rules of the game
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
            

        for (int i = 0; i < players.Count; i++) 
        {
            // When both loops are complete, the computer compares 
            // the hands from top to bottom to decide the outcome
            if (players[i].Hand.Value == 21 && dealer.Hand.Value !=21)
            {
                roundResults.Add(Outcome.Blacjack);
            }
            else if (.Hand.Value > 21)
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

}
