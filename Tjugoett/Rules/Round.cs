using Tjugoett.Cards;
using Tjugoett.Participants;

namespace Tjugoett.Rules;

public class Round
{
    // Here we receive a list of players, the deck and the dealer.
    public List<Outcome> PlayRound(Deck deck, List<Player> players, Dealer dealer)
    {
        // Create an empty list to store the outcome for each player.
        List<Outcome> roundResults = new List<Outcome>();
        
        // Give two cards to ALL players in the list, and to the dealer
        for (int i =0; i < players.Count; i++)
        {
            deck.DealCards(players[i]); // Players get two cards 
        }
        deck.DealCards(dealer); // Dealer gets two cards

        // Player turns - Loop through each player one by one
        for (int i = 0; i < players.Count; i++)
        {
            
            // If a player gets 21 directly, they get Blackjack and skip their turn
            if (players[i].Hand.Value == 21)
            {
                continue;  // We handle the result later in the outcome loop,
                           // so we just jump to the next player here
            }
        
            // if the cards are less than 21 you can get another card
            if (players[i].Hand.Value < 21)
            {   
                // he current player is kept in this loop until they stand or bust
                while (true)
                {
                    Move choice = players[i].GetMove(); // Ask the current player for Hit or Stand

                    if (choice == Move.Hit) // The player gets another card
                    {
                        deck.DealCard(players[i]); // 
                    } 
                    else if (choice == Move.Stand) // The player choose Stand and their loop breaks
                    {
                        break;
                    }
            
                    if (players[i].Hand.Value > 21) // Control the player's value after each card
                    {                               
                        break; // The playern is bust                     
                    }
        
                }
           
            }

        }
            while (true)
            {   
                // Dealer's turn - after the players are done.
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
            
        // Decide the outcome for each player and add it to the results list
        for (int i = 0; i < players.Count; i++) 
        {
            // When both loops are complete, the computer compares 
            // the hands from top to bottom to decide the outcome
            if (players[i].Hand.Value == 21 && dealer.Hand.Value !=21)
            {
                roundResults.Add(Outcome.Blackjack);
            }
            else if (players[i].Hand.Value > 21)
            {
                roundResults.Add(Outcome.Lose);
            }
            else if (dealer.Hand.Value > 21)
            {
                roundResults.Add(Outcome.Win);
            }
            else if (players[i].Hand.Value > dealer.Hand.Value)
            {
                roundResults.Add(Outcome.Win);
            }
            else if (players[i].Hand.Value == dealer.Hand.Value)
            {
                roundResults.Add(Outcome.Push);
            }
            else
            {
                roundResults.Add(Outcome.Lose);
            }
        }
        // Send the final results back to the game loop
        return roundResults;
    
    }

}
