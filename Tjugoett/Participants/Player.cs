using Tjugoett.Rules;

namespace Tjugoett.Participants;

public class Player : Participant
{
    private Func<Move> _askMove; // Func is a type that holds a method - it returns the player's Move (Hit or Stand). 
    public Player(string name, Func<Move> askMove) : base(name) // The method is sent as an argument by Game.
    {
        _askMove = askMove;
    }

    public Move GetMove() //Runs the saved method (AskMove in ConsoleUi) and returns the player's choice to Round. 
    {
        return _askMove();
    }

}
