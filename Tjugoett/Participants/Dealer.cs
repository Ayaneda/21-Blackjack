namespace Tjugoett.Participants;

public class Dealer : Participant
{
    public bool HoleCardHidden { get; private set; } = true;
    public Dealer(string name) : base(name)
    {

    }

    public void RevealHoleCard() // Reveal the dealer's hidden card.
    {
        HoleCardHidden = false;
    }
}
