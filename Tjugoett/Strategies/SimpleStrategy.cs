using Tjugoett.Cards;
using Tjugoett.Rules;

namespace Tjugoett.Strategies;

public class SimpleStrategy : IPlayerStrategy
{

    public string Name => "Enkel strategi"; // name shown to the player at the end of the round
    public Move ChooseMove(Hand hand, Card dealerUpCard) //ComputerPlayer hits when under 17. dealerUpCard is not used, but the interface requires it
    {
        return hand.Value < 17 ? Move.Hit : Move.Stand;
    }
}