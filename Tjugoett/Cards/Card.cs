namespace Tjugoett.Cards;

public class Card
{
    public Suit CardSuit {get;}
    public Rank CardRank {get;}
    public Card (Suit cardSuit, Rank cardRank)
    {
        CardSuit = cardSuit;
        CardRank = cardRank;
    }

    public int Value => (int)CardRank;   //This get the value of the card when it's needed from the enum 

    public override string ToString()    //When the card-object is called, it will show this
    {
        return $"{CardRank} of {CardSuit}";   //Ex. Queen of Spades
    }

}
