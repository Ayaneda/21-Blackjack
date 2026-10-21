using Tjugoett.Rules;
using Tjugoett.Strategies;

namespace Tjugoett.Participants;

public class ComputerPlayer : Player
{

    private IPlayerStrategy _strategy; // the strategy the computer player is using right now
    private Dealer _dealer; //the dealer




    public ComputerPlayer(string name, IPlayerStrategy strategy, Dealer dealer) : base(name) // Constructor: name, strategy and dealer
    {
        _strategy = strategy; // saves the strategy so GetMove() can use it later
        _dealer = dealer; //is needed to see the dealer's open card
    }


    public void SetStrategy(IPlayerStrategy strategy) //Swap strategy
    {
        _strategy = strategy; // replace the old strategy with the new one
    }


    public override Move GetMove()  // Round calls this. Instead of asking a human for a move, the computer player's strategy is asked for a move.
    {
        return _strategy.ChooseMove(Hand, _dealer.Hand.Cards[0]);
    }
}