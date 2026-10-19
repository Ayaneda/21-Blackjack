namespace Tjugoett.Participants;

public class ComputerPlayer : Player
{

    private IPlayerStrategy _strategy; // the strategy the computer player is using right now




    public ComputerPlayer(string name, IPlayerStrategy strategy) : base(name) // Constructor: name and strategy.
    {
        //a new construktor has to be build in Player for this to work
    }


    public void SetStrategy(IPlayerStrategy strategy) //Swap strategy
    {

    }


    public override Move GetMove()  //Round calls this. Ask the strategy instead of the console.
    {
        // return _strategy.ChooseMove(...);
    }
}