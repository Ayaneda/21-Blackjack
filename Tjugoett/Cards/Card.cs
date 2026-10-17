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

    public int Value
    {
        get
        {
            if(CardRank == Rank.Ess) return 11;    //because how enum works, I need to set deffault value to 11 when it's called
            if(CardRank >= Rank.Knekt && CardRank <=Rank.Kung) return 10;  //because how enum works, I need to set deffault value to 10 when it's called
            return (int)CardRank;   //other cards have right the value
        }
    }

    public override string ToString()    //When the card-object is called, it will show this
    {
        return $"{CardRank} av {CardSuit}";   //Ex. Queen of Spades
    }

}
