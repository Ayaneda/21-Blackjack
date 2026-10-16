using Tjugoett.Rules;

namespace Tjugoett.Participants;

public class Player : Participant
{
    private Func<Participant, Move> _askMove; // Func is a type that holds a method - it takes a participant and returns the player's Move (Hit or Stand).
    public Player(string name, Func<Participant, Move> askMove) : base(name) // The method is sent as an argument by Game.
    {
        _askMove = askMove;
    }

    public Move GetMove() //Runs the saved method (AskMove in ConsoleUi) and returns the player's choice to Round. 
    {
        return _askMove(this); //// "this" is the current player. It is sent to AskMove so the right hand can be shown.
    }

}
