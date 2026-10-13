namespace Tjugoett.Cards;

public class Card
{
    public Suit CardSuite {get;}
    public Rank CardRank {get;}
    public Card (Suit cardSuite, Rank cardRank)
    {
        CardSuite = cardSuite;
        CardRank = cardRank;
    }

    public int Value => (int)CardRank;   //This get the value of the card when it's needed from the enum 

    public override string ToString()    //When the card-object is called, it will show this
    {
        return $"{CardRank} of {CardSuite}";   //Ex. Queen of Spades
    }

}
