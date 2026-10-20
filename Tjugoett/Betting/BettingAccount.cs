namespace Tjugoett.Betting;

using Tjugoett.Rules;
public class BettingAccount
{
    public decimal Chips { get; private set; } // Available player chips.

    public decimal CurrentBet { get; private set; } // The amount currently bet by the player.

    public BettingAccount(decimal startingChips = 100) // Create an account with 100 chips by default.
    {
        if (startingChips < 0) // Prevent a negative startning balance.
        {
            throw new ArgumentException("Chips cannot be negative.");
        }
        Chips = startingChips;
    }

    public bool PlaceBet(decimal amount) // Place a bet before th round starts.
    {
        if (amount <= 0 || amount > Chips || CurrentBet != 0) // Reject zero, negative, too large or repeated bets.
        {
            return false;
        }
        Chips -= amount; // Remove the bet from available chips.

        CurrentBet = amount; // Save the bet for the current round.

        return true;
    }
    public void SettleBet(Outcome outcome) // Pay chips based on the round result.
    {
        if (CurrentBet == 0)
        {
            return;  // No active bet.
        }

        switch (outcome)
        {
            case Outcome.Win:
                Chips += CurrentBet * 2; // Return bet and winnings.
                break;

            case Outcome.Lose: // The player loses the bet.
                break;

            case Outcome.Push: // Return the bet.
                Chips += CurrentBet;
                break;

            case Outcome.Blackjack:
                Chips += CurrentBet + CurrentBet * 1.5m; // Return bet and 3:2 winnings.
                break;

        }
        CurrentBet = 0; // Reset the bet after the round.
    }


}