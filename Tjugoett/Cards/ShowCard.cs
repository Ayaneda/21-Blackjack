namespace Tjugoett.Cards;

public class ShowCard
{
    public static string PrintCard(string RankSymbol, string SuitSymbol)   //Format for card
    {
        string color = (SuitSymbol == "♥" || SuitSymbol == "♦") ? "\u001b[31m" : ""; //Set colour depending of Suit
        string reset = "\u001b[0m";                                                  //Set colour to deffault again
        string card = $"┌───────┐\n" +
                      $"│{color}{RankSymbol,-2}{reset}     │\n" +
                      $"│       │\n" +
                      $"│   {color}{SuitSymbol}{reset}   │\n" +
                      $"│       │\n" +
                      $"│     {color}{RankSymbol,2}{reset}│\n" +
                      $"└───────┘";
        return card;
    }
    public static string ShowHideCard() //Format for hidden card
    {
        string hiddenCard = "┌───────┐\n" +
                            "│▪▪▪▪▪▪▪│\n" +
                            "│▪▪▪▪▪▪▪│\n" +
                            "│▪▪▪▪▪▪▪│\n" +
                            "│▪▪▪▪▪▪▪│\n" +
                            "│▪▪▪▪▪▪▪│\n" +
                            "└───────┘";
        return hiddenCard;
    }
}