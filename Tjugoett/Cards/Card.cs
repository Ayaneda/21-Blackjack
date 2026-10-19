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

    public string RankToSwedish
    {
        get
        {
            return CardRank switch
            {
                Rank.Jack => "Knekt",
                Rank.Queen => "Dam",
                Rank.King => "Kung",
                Rank.Ace => "Ess",
                _        => CardRank.ToString()  
            };
             
        }
         
    }
    public string SuitToSwedish
    {
        get
        {
            return CardSuit switch
            {
                Suit.Clubs => "Klöver",
                Suit.Spades => "Späder",
                Suit.Diamonds => "Ruter",
                Suit.Hearts => "Hjärter",
                _           => CardSuit.ToString()
            };
        }
    }

    public override string ToString()    //When the card-object is called, it will show this
    {
        return $"{RankToSwedish} av {SuitToSwedish}";   //Ex. Dam av Späder
    }

}
