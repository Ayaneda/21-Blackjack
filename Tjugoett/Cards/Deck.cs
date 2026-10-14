using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Tjugoett.Participants;

namespace Tjugoett.Cards;

public class Deck
{
    
    private List <Card> _deck = new List<Card>();
    
    private void _buildDeck()  //method to build a deck. Private because anyone should doing outside this method
    {
        foreach(Suit cardSuit in Enum.GetValues(typeof(Suit)))   //Getting values from enum Suit
        {
            foreach(string rankName in Enum.GetNames(typeof(Rank)))  //Getting names from enum Rank as loop, new variable rankName
            {
                Rank cardRank = (Rank)Enum.Parse(typeof(Rank), rankName); //cardRank get value of rankName(name) as cardRank

                Card newCard = new Card(cardSuit,cardRank); //create a new card with the constructor
                _deck.Add(newCard); //add newCard to the deck
            }
        }
    }
    private void _shuffle()
    {
        _deck.Clear();          //this clear the list
        _buildDeck();           //this create a new deck (52 cards)
        Random.Shared.Shuffle(CollectionsMarshal.AsSpan(_deck));   //this shuffle the new deck randomly 
        //I want to add that the code above uses Fisher-Yates algorithm according to my information source(Gemini)
    }
    public void RemoveCard(out Card removedCard)    //This method take the card that is in top(last index)
    {
        removedCard = _deck[^1];   //^1(the first from the last index) so it set the item in the variable
        _deck.RemoveAt(_deck.Count - 1);     //Last, it remove the item from the list
    }

    public void DealCard(Participant participant)  //This method draw a card from deck and added to the participant(player/dealer)
    {
        RemoveCard(out Card card);          //Remove a card from _deck
        participant.ReceiveCard(card);      //Add card removed to participant(player/dealer)
    }
    public void DealCards(Participant participant)   //This method call DealCard twice when the round start and all player need 2 cards.
    {
        DealCard(participant);
        DealCard(participant);
    }
    public override string ToString()     //This tells how many card are left in deck.
    {
        return $"Kortleken har {_deck.Count - 1}/52 kort.";
    }
    

}

