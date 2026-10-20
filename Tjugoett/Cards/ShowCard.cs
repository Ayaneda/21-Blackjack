namespace Tjugoett.Cards;

public class ShowCard
{
    
    public static string PrintCard(string RankSymbol, string SuitSymbol)
    {
        string card = $"┌───────┐\n" +
                      $"│{RankSymbol,-2}     │\n" +
                      $"│       │\n" +
                      $"│   {SuitSymbol}   │\n" +
                      $"│       │\n" +
                      $"│     {RankSymbol,2}│\n" +
                      $"└───────┘";
        return card;
    }
    public static string PrintHideCard()
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