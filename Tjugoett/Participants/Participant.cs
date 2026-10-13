using Tjugoett.Cards;

namespace Tjugoett.Participants;

public class Participant
{
    public string Name { get; }
    public Hand Hand { get; }

    public Participant(string name)
    {
        Name = name;
        Hand = new Hand();
    }


    // A card is received and added to the hand
    public void ReceiveCard(Card card)
    {
        Hand.AddCard(card);
    }

}
