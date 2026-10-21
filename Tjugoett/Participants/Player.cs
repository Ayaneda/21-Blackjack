using Tjugoett.Rules;

namespace Tjugoett.Participants;

public class Player : Participant
{
    private Func<Participant, Move>? _askMove; // Func is a type that holds a method - it takes a participant and returns the player's Move (Hit or Stand)
    public Player(string name, Func<Participant, Move> askMove) : base(name) // Constructor used when a human player is created. The method askMove is sent as an argument by Game
    {
        _askMove = askMove;
    }

    protected Player(string name) : base(name) // A second constructor used by ComputerPlayer, which doesn't ask the console for av move
    {
    }

    public virtual Move GetMove() // Only for human players. ComputerPlayer has its own overridden GetMove()
    {
        if (_askMove == null) //Only happens if a Player was created with the name-only constructor (meant for ComputerPlayer)
        {
            throw new InvalidOperationException($"Player '{Name}' was created without a move method. Use the constructor with askMove, or create a ComputerPlayer.");
        }
        return _askMove(this);
    }

}
