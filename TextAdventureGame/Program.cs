namespace TextAdventureGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, adventurer!");

            Console.WriteLine("What is your name?");
            string playerName = Console.ReadLine();

            Console.WriteLine($"Greetings, {playerName}! Your adventure begins...");

            Console.WriteLine("You stand at the entrance of a dark cave...");
            Console.WriteLine("Do you want to enter? (yes/no)");
            string choice = Console.ReadLine();

            if (choice == "yes")
            {
                Console.WriteLine("You bravely step into the darkness...");
                Console.WriteLine("You find 10 gold coins!");
            }
            else
            {
                Console.WriteLine("You decide to stay at camp. Probably safer.");
            }
        }
    }
}