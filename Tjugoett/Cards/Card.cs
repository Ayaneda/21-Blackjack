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
            if(CardRank == Rank.Ace) return 11;    //because how enum works, I need to set deffault value to 11 when it's called
            if(CardRank >= Rank.Jack && CardRank <= Rank.King) return 10;  //because how enum works, I need to set deffault value to 10 when it's called
            return (int)CardRank;   //other cards have right the value
        }
    }

    public string RankToSwedish(Rank rank)
    {
        {
            switch (rank)
            {
                case Rank.D2: return "D2";
                case Rank.D3: return "D3";
                case Rank.D4: return "D4";
                case Rank.D5: return "D5";
                case Rank.D6: return "D6";
                case Rank.D7: return "D7";
                case Rank.D8: return "D8";
                case Rank.D9: return "D9";
                case Rank.D10: return "D10";
                case Rank.Jack: return "Knekt";
                case Rank.Queen: return "Dam";
                case Rank.King: return "King";
                case Rank.Ace: return "Ass";
                default: return rank.ToString();
            }
        }
    }
    public string SuitToSwedish(Suit suit)
    {
        switch (suit)
        {
            case Suit.Clubs: return "Klöver";
            case Suit.Spades: return "Späder";
            case Suit.Diamonds: return "Ruter";
            case Suit.Hearts: return "Hjärter";
            default: return suit.ToString();
        }
    }

    public override string ToString()    //When the card-object is called, it will show this
    {
        return $"{RankToSwedish} av {SuitToSwedish}";   //Ex. Dam av Späder
    }

}
