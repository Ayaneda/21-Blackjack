using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace Tjugoett.Cards;

public class Deck
{
    //Bygger leken, blandar, delar ut, vet hur många kort som är kvar
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




}

