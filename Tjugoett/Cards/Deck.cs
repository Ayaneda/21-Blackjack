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
}

