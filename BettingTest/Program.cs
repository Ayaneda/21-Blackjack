
using Tjugoett.Betting;
using Tjugoett.Rules;

// Test payouts for different round results.
TestBet(20, Outcome.Win, 120);
TestBet(20, Outcome.Lose, 80);
TestBet(20, Outcome.Push, 100);
TestBet(20, Outcome.Blackjack, 130);
TestBet(5, Outcome.Blackjack, 107.5m); // Test 3:2 payout.

// Test invalid bets.
BettingAccount account = new BettingAccount(100);

Console.WriteLine($"Zero bet rejected: {!account.PlaceBet(0)}");
Console.WriteLine($"Too large bet rejected: {!account.PlaceBet(150)}");

// Test one bet and compare the balance.
static void TestBet(int amount, Outcome outcome, decimal expected)
{
    BettingAccount account = new BettingAccount(100);

    bool accepted = account.PlaceBet(amount);
    account.SettleBet(outcome);

    bool passed = accepted &&
                  account.Chips == expected &&
                  account.CurrentBet == 0;

    Console.WriteLine(
        $"{outcome}: Expected {expected}, Actual {account.Chips}, " +
        $"Result: {(passed ? "PASS" : "FAIL")}"
    );
}
