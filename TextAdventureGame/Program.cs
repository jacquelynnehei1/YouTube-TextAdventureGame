namespace TextAdventureGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain == true)
            {
                Game game = new Game();
                game.Run();  

                Console.WriteLine("\nPlay again? (yes/no)");
                string response = Console.ReadLine().ToLower();

                if (response != "yes")
                {
                    playAgain = false;
                    Console.WriteLine("Thanks for playing!");
                }  
            }
        }
    }
}