namespace TextAdventureGame
{
    internal class Program
    {
        enum Location
        {
            Camp,
            Cave
        }

        static void DisplayInventory(string[] inventory)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    Console.WriteLine($"[{i}] EMPTY");   
                }
                else
                {
                    Console.WriteLine($"[{i}] {inventory[i]}");
                }
            }
        }

        static bool RemoveItem(string[] inventory, string itemName)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == itemName)
                {
                    inventory[i] = null;
                    return true;
                }
            }

            return false;
        }

        static int FindEmptySlot(string[] inventory)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }

        static void Main(string[] args)
        {
            bool isPlaying = true;

            int health = 10;
            int gold = 0;

            Location currentLocation = Location.Camp;

            string[] inventory = new string[10];

            Console.WriteLine("Hello, adventurer!");

            Console.WriteLine("What is your name?");
            string playerName = Console.ReadLine();

            Console.WriteLine($"Greetings, {playerName}! Your adventure begins...");

            while (isPlaying == true)
            {
                if (currentLocation == Location.Camp)
                {
                    Console.WriteLine("You are at the campfire. Enter the cave? (yes/no/stats/quit)");    
                }
                else if (currentLocation == Location.Cave)
                {
                    Console.WriteLine("You are in a dark cave. Go back to camp? (yes/no/stats/quit)");
                }

                string choice = Console.ReadLine().ToLower();

                if (choice == "yes")
                {
                    if (currentLocation == Location.Camp)
                    {
                        currentLocation = Location.Cave;
                        Console.WriteLine("You bravely step into the darkness and trip on a rock, losing 2 health.");
                        health = health - 2;
                    }
                    else if (currentLocation == Location.Cave)
                    {
                        currentLocation = Location.Camp;
                        Console.WriteLine("The cave is dark and scary. You head back to camp to warm by the fire.");
                    }
                    
                }
                else if (choice == "no")
                {
                    if (currentLocation == Location.Camp)
                    {
                        Console.WriteLine("You decide to stay at camp. Probably safer.");
                    }
                    else if (currentLocation == Location.Cave)
                    {
                        Console.WriteLine("You decide to stay in the cave. It's quite dark.");   
                    }
                }
                else if (choice == "inventory")
                {
                    DisplayInventory(inventory);
                }
                else if (choice == "search")
                {
                    if (currentLocation == Location.Cave)
                    {
                        int emptySlot = FindEmptySlot(inventory);

                        if (emptySlot >= 0)
                        {
                            Console.WriteLine("You search the cave and find a rusty key. You put it in your pack.");
                            inventory[emptySlot] = "rusty key";
                        }
                        else
                        {
                            Console.WriteLine("You search the cave and find a rusty key but your inventory is too full to pick it up.");
                        }

                        Console.WriteLine("You also find 10 gold coins!");
                        gold = gold + 10;
                    }
                    else
                    {
                        Console.WriteLine("You search but find nothing.");
                    }
                }
                else if (choice.StartsWith("drop "))
                {
                    string itemName = choice.Substring(5);
                    bool isRemoved = RemoveItem(inventory, itemName);

                    if (isRemoved == true)
                    {
                        Console.WriteLine($"You drop the {itemName}.");
                    }
                    else
                    {
                        Console.WriteLine($"You don't have a {itemName}.");
                    }
                }
                else if (choice == "stats")
                {
                    Console.WriteLine($"Health: {health}");
                    Console.WriteLine($"Gold: {gold}");
                    Console.WriteLine($"Location: {currentLocation}");
                }
                else if (choice == "quit")
                {
                    isPlaying = false;
                    Console.WriteLine("Your adventure has come to an end...");
                }
                else
                {
                    Console.WriteLine("I don't understand that.");
                }

                if (health <= 0)
                {
                    Console.WriteLine("You collapse from exhaustion. Game over.");
                    isPlaying = false;
                }
            }
        }
    }
}