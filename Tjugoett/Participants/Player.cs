using Tjugoett.Rules;

namespace Tjugoett.Participants;

public class Player : Participant
{
    public Player(string name) : base(name)
    {

    }

    public Move GetMove()
    {
        return Move.Stand; // TODO: Move.Stand is temporary until we decide how the player's choice reaches Round.
    }

}
