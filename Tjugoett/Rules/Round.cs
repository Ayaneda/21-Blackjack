using Tjugoett.Cards;
using Tjugoett.Participants;
using Tjugoett.UI;

namespace Tjugoett.Rules;

public class Round
{
    public ConsoleUi ConsoleUi;
    public  Round()
    {
        ConsoleUi = new ConsoleUi();
    }
    // Here we receive a list of players, the deck and the dealer.
    public List<Outcome> PlayRound(Shoes deck, List<Player> players, Dealer dealer)
    {
        // Create an empty list to store the outcome for each player.
        List<Outcome> roundResults = new List<Outcome>();

        // Give two start cards to ALL players in the list, and to the dealer
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                // Loop twice to deal two cards
                Card card = deck.DealCard();
                players[i].Hand.AddCard(card);
            }
        }
        for (int j = 0; j < 2; j++)
        {
            // Gives two card to the dealder
            Card delarCard = deck.DealCard();
            dealer.Hand.AddCard(delarCard);
        }

        // Player turns - Loop through each player one by one
        for (int i = 0; i < players.Count; i++)
        {
            // If a player gets 21 directly, they get Blackjack and skip their turn
            if (players[i].Hand.Value == 21 && players[i].Hand.Cards.Count == 2)
            {
                // We handle the result later in the outcome loop,
                // so we just jump to the next player here
                continue;
            }

            // If the cards are less than 21 you can get another card
            if (players[i].Hand.Value < 21)
            {   
                // The current player is kept in this loop until they stand or bust
                while (true)
                {
                    Move choice = players[i].GetMove(); // Ask the current player for Hit or Stand

                    if (choice == Move.Hit) // The player gets another card
                    {
                        // Take a car from the deck and gives that to the player.
                        Card drawnCard = deck.DealCard();
                        players[i].Hand.AddCard(drawnCard);
                        
                    } 
                    else if (choice == Move.Stand) // The player chooses Stand and their loop breaks
                    {
                        break;
                    }

                    if (players[i].Hand.Value == 21) // Control the player's value after each card
                    {    
                        ConsoleUi.ShowMessage ("Du har fått Blackjack!!! ");                         
                        break; // The player is bust                     
                    }
                    if (players[i].Hand.Value > 21) // Control the player's value after each card
                    {    
                        ConsoleUi.ShowMessage ("Du har blivit tjock!!! ");                            
                        break; // The player is bust                     
                    }
                }
            }
        }

        // Dealer's turn - after the players are done.
        dealer.RevealHoleCard(); // Turning the hole card first

        while (true)
        {   
            if (dealer.ShouldHit())
            {
                // Takes a card from the shoe
                Card dealCard= deck.DealCard();

                // Gives the card to the dealer manually
                dealer.Hand.AddCard(dealCard);
            }
            else
            {
                break; // The dealer Stand or is bust.
            }
        }

        // Decide the outcome for each player and add it to the results list
        for (int i = 0; i < players.Count; i++) 
        {
            // The player has a natural Blackjack, but the dealer does not.
            if (players[i].Hand.Value == 21 && players[i].Hand.Cards.Count == 2 && !(dealer.Hand.Value == 21 && dealer.Hand.Cards.Count == 2))
            {
                roundResults.Add(Outcome.Blackjack);
            }
            else if (players[i].Hand.Value > 21) // Player is bust.
            {
                roundResults.Add(Outcome.Lose);
            }
            else if (dealer.Hand.Value > 21) // The dealer is bust
            {
                roundResults.Add(Outcome.Win);
            }
            // Both have a natural Blackjack (21 on two cards)
            else if (players[i].Hand.Value == 21 && players[i].Hand.Cards.Count == 2 && dealer.Hand.Value == 21 && dealer.Hand.Cards.Count == 2)
            {
               roundResults.Add(Outcome.Push);
            }
            // The dealer has a natural Blackjack (2 cards), but the player has a regular 21 (3+ cards)
            else if (dealer.Hand.Value == 21 && dealer.Hand.Cards.Count == 2 && !(players[i].Hand.Value == 21 && players[i].Hand.Cards.Count == 2))
            {
                roundResults.Add(Outcome.Lose);
            }
            else if (players[i].Hand.Value > dealer.Hand.Value) // The player has a higher score than the dealer
            {
                roundResults.Add(Outcome.Win);
            }
            else if (players[i].Hand.Value == dealer.Hand.Value) // Tie score (both have the same cards value)
            {
                roundResults.Add(Outcome.Push);
            }
            else // Otherwise, the dealer has a higher score.
            {
                roundResults.Add(Outcome.Lose);
            }
        }

        // Send the final results back to the game loop
        return roundResults;
   
    }


}