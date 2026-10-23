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
    public void HideHoleCard() // Hid the dealer's hole card agin before a new round.
    {
        HoleCardHidden = true;
    }

    public bool ShouldHit() // Returns true if the dealer should take another card.
    {
        return Hand.Value < 17;
    }

}