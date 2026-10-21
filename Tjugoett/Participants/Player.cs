
using Tjugoett.Betting;
using Tjugoett.Rules;

namespace Tjugoett.Participants;

public class Player : Participant
{
    private Func<Participant, Move>? _askMove; // Stores the method used to get the player's move.

    public BettingAccount Betting { get; private set; } // Each player has their own betting account.

    public Player(string name, Func<Participant, Move> askMove, decimal startingChips = 100) : base(name) // Constructor for human players.
    {
        _askMove = askMove;
        Betting = new BettingAccount(startingChips); // Creates an account with the player's starting chips.
    }

    protected Player(string name) : base(name) // Constructor for computer players.
    {
        Betting = new BettingAccount(); // Creates a betting account for computer players.
    }

    public virtual Move GetMove() // Returns the player's move and allows computer players to override it.
    {
        if (_askMove == null) // Checks if the player has a move method.
        {
            throw new InvalidOperationException(
                $"Player '{Name}' was created without a move method.");
        }

        return _askMove(this); // Returns the player's choice (Hit or Stand).
    }
}
