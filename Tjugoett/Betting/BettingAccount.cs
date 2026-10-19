namespace Tjugoett.Betting;

public class BettingAccount
{
    public int Chips { get; private set; } // The number och the player has available.

    public int CurrentBet { get; private set; } // The amount of chips currently bet by the player.

    public BettingAccount(int startingChips = 100) // Creat an account with 100 chips by default.
    {
        if (startingChips < 0) // Prevent a negative startning balance.
        {
            throw new ArgumentException("Chips cannot be negative.");
        }
        Chips = startingChips;
    }

    public bool PlaceBet(int amount) // Place a bet before th round start.
    {
        if (amount <= 0 || amount > Chips || CurrentBet != 0) // Reject zero, negative, too large or repeated bets.
        {
            return false;
        }
        Chips -= amount; // Remove the bet from available chips.

        CurrentBet = amount; // Save the bet for the current round.

        return true;
    }


}