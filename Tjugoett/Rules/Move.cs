namespace Tjugoett.Rules;

public enum Move
{
    Hit, Stanna
}

public class Moves
{
    public Move SpelarensVal()
    {   
        // Here the user chooses whether to take a new card or not.
        Console.WriteLine("Vad är dit val (Hit/Stanna): ");
        string? val = Console.ReadLine();
       
        // Checks if the user chose Hit
        if (val == "Hit")
        {
            Console.WriteLine("Du valde ett nytt kort! ");
            return Move.Hit;
        }
        // Checks if the has chosen Stanna
        if (val == "Stanna")
        {
            Console.WriteLine("Du valda att stanna! ");
            return Move.Stanna;
        }
        // Forces the user to use either Hit or Stanna
        else
        {
            Console.WriteLine("Fel! Du kan bara välja mellan Hit eller Stanna");
            return Move.Stanna;
        }

        
    }

}



