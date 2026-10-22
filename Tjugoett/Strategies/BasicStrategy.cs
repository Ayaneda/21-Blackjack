using Tjugoett.Cards;
using Tjugoett.Rules;

namespace Tjugoett.Strategies;

// Basic strategy with only Hit and Stand.
// Double counts as Hit, "Ds" (double, else stand) counts as Stand
public class BasicStrategy : IPlayerStrategy
{

    public string Name => "Grundstrategi"; // name shown to the player at the end of the round
    private const Move H = Move.Hit;   //const means that the value can't be changed
    private const Move S = Move.Stand;


    // Hard hands 12-16. Row = hand value - 12. Column = dealer card value - 2.
    private static readonly Move[,] HardTable =  // no Ace, or Ace counted as 1
    {
        // dealer:  2  3  4  5  6  7  8  9  10 A
        // column:  0  1  2  3  4  5  6  7  8  9
        /* 12 */  { H, H, S, S, S, H, H, H, H, H },  // row 0
        /* 13 */  { S, S, S, S, S, H, H, H, H, H },  // row 1
        /* 14 */  { S, S, S, S, S, H, H, H, H, H },  // row 2
        /* 15 */  { S, S, S, S, S, H, H, H, H, H },  // row 3
        /* 16 */  { S, S, S, S, S, H, H, H, H, H },  // row 4
    };

    // Soft hands 12-18. Row = hand value - 12. Column = dealer card value - 2.
    private static readonly Move[,] SoftTable =  // an Ace counted as 11
    {
        // dealer:  2  3  4  5  6  7  8  9  10 A
        // column:  0  1  2  3  4  5  6  7  8  9
        /* 12 */  { H, H, H, H, H, H, H, H, H, H },  // row 0
        /* 13 */  { H, H, H, H, H, H, H, H, H, H },  // row 1
        /* 14 */  { H, H, H, H, H, H, H, H, H, H },  // row 2
        /* 15 */  { H, H, H, H, H, H, H, H, H, H },  // row 3
        /* 16 */  { H, H, H, H, H, H, H, H, H, H },  // row 4
        /* 17 */  { H, H, H, H, H, H, H, H, H, H },  // row 5
        /* 18 */  { S, S, S, S, S, S, S, H, H, H },  // row 6
    };

    public Move ChooseMove(Hand hand, Card dealerUpCard)
    {
        int value = hand.Value;
        int column = dealerUpCard.Value - 2; // - 2 because arrays start at 0, but the dealer's lowest card is 2

        if (hand.IsSoft)
        {
            if (value >= 19) // soft 19-21 always stands
            {
                return S;
            }
            return SoftTable[value - 12, column];
        }

        if (value <= 11) // can never bust, always hit
        {
            return H;
        }
        if (value >= 17) // hard 17-21 always stands
        {
            return S;
        }
        return HardTable[value - 12, column];
    }
}