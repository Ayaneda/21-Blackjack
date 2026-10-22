using Tjugoett.Cards;
using Tjugoett.Rules;

namespace Tjugoett.Strategies;

public interface IPlayerStrategy
{
    string Name { get; } // every strategy must have a name that can be shown to the player
    Move ChooseMove(Hand hand, Card dealerUpCard);
}