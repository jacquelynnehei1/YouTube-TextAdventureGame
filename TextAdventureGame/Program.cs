namespace TextAdventureGame
{
    internal class Program
    {
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

            Player player = new Player(10, 0, Location.Camp);

            Console.WriteLine("Hello, adventurer!");

            Console.WriteLine("What is your name?");
            string playerName = Console.ReadLine();

            Console.WriteLine($"Greetings, {playerName}! Your adventure begins...");

            while (isPlaying == true)
            {
                if (player.CurrentLocation == Location.Camp)
                {
                    Console.WriteLine("You are at the campfire. Enter the cave? (yes/no/stats/quit)");    
                }
                else if (player.CurrentLocation == Location.Cave)
                {
                    Console.WriteLine("You are in a dark cave. Go back to camp? (yes/no/stats/quit)");
                }

                string choice = Console.ReadLine().ToLower();

                if (choice == "yes")
                {
                    if (player.CurrentLocation == Location.Camp)
                    {
                        player.CurrentLocation = Location.Cave;
                        Console.WriteLine("You bravely step into the darkness and trip on a rock, losing 2 health.");
                        player.TakeDamage(2);
                    }
                    else if (player.CurrentLocation == Location.Cave)
                    {
                        player.CurrentLocation = Location.Camp;
                        Console.WriteLine("The cave is dark and scary. You head back to camp to warm by the fire.");
                    }
                    
                }
                else if (choice == "no")
                {
                    if (player.CurrentLocation == Location.Camp)
                    {
                        Console.WriteLine("You decide to stay at camp. Probably safer.");
                    }
                    else if (player.CurrentLocation == Location.Cave)
                    {
                        Console.WriteLine("You decide to stay in the cave. It's quite dark.");   
                    }
                }
                else if (choice == "inventory")
                {
                    DisplayInventory(player.Inventory);
                }
                else if (choice == "search")
                {
                    if (player.CurrentLocation == Location.Cave)
                    {
                        int emptySlot = FindEmptySlot(player.Inventory);

                        if (emptySlot >= 0)
                        {
                            Console.WriteLine("You search the cave and find a rusty key. You put it in your pack.");
                            player.Inventory[emptySlot] = "rusty key";
                        }
                        else
                        {
                            Console.WriteLine("You search the cave and find a rusty key but your inventory is too full to pick it up.");
                        }

                        Console.WriteLine("You also find 10 gold coins!");
                        player.AddGold(10);
                    }
                    else
                    {
                        Console.WriteLine("You search but find nothing.");
                    }
                }
                else if (choice.StartsWith("drop "))
                {
                    string itemName = choice.Substring(5);
                    bool isRemoved = RemoveItem(player.Inventory, itemName);

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
                    Console.WriteLine($"Health: {player.Health}");
                    Console.WriteLine($"Gold: {player.Gold}");
                    Console.WriteLine($"Location: {player.CurrentLocation}");
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

                if (player.Health <= 0)
                {
                    Console.WriteLine("You collapse from exhaustion. Game over.");
                    isPlaying = false;
                }
            }
        }
    }
}