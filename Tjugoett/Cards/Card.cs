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

    public string RankSymbol
    {
        get
        {
            return CardRank switch
            {
                Rank.D2 => "2",
                Rank.D3 => "3",
                Rank.D4 => "4",
                Rank.D5 => "5",
                Rank.D6 => "6",
                Rank.D7 => "7",
                Rank.D8 => "8",
                Rank.D9 => "9",
                Rank.D10 => "10",
                Rank.Jack => "J",
                Rank.Queen => "Q",
                Rank.King => "K",
                Rank.Ace => "A",
                _        => CardRank.ToString()  
            };
             
        }
         
    }
    public string SuitSymbol
    {
        get
        {
            return CardSuit switch
            {
                Suit.Clubs => "♣",
                Suit.Spades => "♠",
                Suit.Diamonds => "♦",
                Suit.Hearts => "♥",
                _           => CardSuit.ToString()
            };
        }
    }

    public override string ToString()    //When the card-object is called, it will show this
    {
        return $"{ShowCard.PrintCard(RankSymbol, SuitSymbol)}";
    }

}
