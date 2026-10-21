using Tjugoett.Cards;
using Tjugoett.Rules;

namespace Tjugoett.Strategies;

public class SimpleStrategy : IPlayerStrategy
{
    public Move ChooseMove(Hand hand, Card dealerUpCard) //ComputerPlayer hits when under 17. dealerUpCard is not used, but the interface requires it
    {
        return hand.Value < 17 ? Move.Hit : Move.Stand;
    }
}