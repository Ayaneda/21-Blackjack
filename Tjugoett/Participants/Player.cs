
using Tjugoett.Betting;
using Tjugoett.Rules;

namespace Tjugoett.Participants;

public class Player : Participant
{
    private Func<Participant, Move>? _askMove; // Func is a type that holds a method - it takes a participant and returns the player's Move (Hit or Stand)

    public BettingAccount Betting { get; private set; } // Each player has their own betting account.

    public Player(string name, Func<Participant, Move> askMove, decimal startingChips = 100) : base(name) // Constructor used when a human player is created. The method askMove is sent as an argument by Game.
    {
        _askMove = askMove;
        Betting = new BettingAccount(startingChips); // Creates an account with the player's starting chips.
    }

    protected Player(string name) : base(name) // A second constructor used by ComputerPlayer, which doesn't ask the console for a move.
    {
        Betting = new BettingAccount(); // Creates a betting account for computer players.
    }

    public virtual Move GetMove() //  Only for human players. ComputerPlayer has its own overridden GetMove()

    {
        if (_askMove == null) // Only happens if a Player was created with the name-only constructor (meant for ComputerPlayer).
        {
            throw new InvalidOperationException(
                $"Player '{Name}' was created without a move method.");
        }

        return _askMove(this); // Returns the player's choice (Hit or Stand).
    }
}
