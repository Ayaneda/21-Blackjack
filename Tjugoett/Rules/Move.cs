namespace Tjugoett.Rules;

public enum Move
{
    Hit, Stand
}

public class Moves
{
    public Move SpelarensVal()
    {   
        // Here the user chooses whether to take a new card or not.
        Console.WriteLine("Vad är dit val (Hit/Stand): ");
        string? val = Console.ReadLine();
       
        // Checks if the user chose Hit
        if (val == "Hit")
        {
            Console.WriteLine("Du valde ett nytt kort! ");
            return Move.Hit;
        }
        // Checks if the has chosen Stanna
        if (val == "Stand")
        {
            Console.WriteLine("Du valda att stanna! ");
            return Move.Stand;
        }
        // Forces the user to use either Hit or Stanna
        else
        {
            Console.WriteLine("Fel! Du kan bara välja mellan Hit eller Stanna");
            return Move.Stand;
        }

        
    }

}



