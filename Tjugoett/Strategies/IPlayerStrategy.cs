using Tjugoett.Cards;
using Tjugoett.Rules;

namespace Tjugoett.Strategies;

public interface IPlayerStrategy
{
    Move ChooseMove(Hand hand, Card dealerUpCard);
}