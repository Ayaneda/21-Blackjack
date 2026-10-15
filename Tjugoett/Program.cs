using Tjugoett.Cards;
using Tjugoett.Participants;


Deck deck = new Deck();
Player player = new Player("Anna");
Dealer dealer = new Dealer("Givaren");

deck.DealCards(player);
deck.DealCards(dealer);

Console.WriteLine($"{player.Name}: {player.Hand.Value}");
Console.WriteLine($"{dealer.Name}: {dealer.Hand.Value}");
Console.WriteLine(deck);
Console.WriteLine($"{deck.deck.Count}");