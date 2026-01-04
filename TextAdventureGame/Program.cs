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

            Room camp = new Room("Camp", "A warm fire crackles nearby. You feel safe here.");
            Room cave = new Room("Cave", "A dark, echoing space. Water drips from the ceiling.");

            cave.Items.Add("rusty key");

            camp.Exit = cave;
            cave.Exit = camp;

            Player player = new Player(10, 0, camp);

            Console.WriteLine("Hello, adventurer!");

            Console.WriteLine("What is your name?");
            string playerName = Console.ReadLine();

            Console.WriteLine($"Greetings, {playerName}! Your adventure begins...");

            while (isPlaying == true)
            {
                Console.WriteLine(player.CurrentRoom.Description);

                string choice = Console.ReadLine().ToLower();

                if (choice == "yes")
                {
                    if (player.CurrentRoom.Exit != null)
                    {
                        player.CurrentRoom = player.CurrentRoom.Exit;
                        Console.WriteLine($"You travel to {player.CurrentRoom.Name}");
                    }
                    else
                    {
                        Console.WriteLine("There's nowhere to go from here.");
                    }
                }
                else if (choice == "no")
                {
                    Console.WriteLine($"You decide to stay in {player.CurrentRoom.Name}");
                }
                else if (choice == "inventory")
                {
                    DisplayInventory(player.Inventory);
                }
                else if (choice == "search")
                {
                    if (player.CurrentRoom.Items.Count == 0)
                    {
                        Console.WriteLine("You search but find nothing.");
                    }
                    else
                    {
                        foreach (string item in player.CurrentRoom.Items)
                        {
                            int emptySlot = FindEmptySlot(player.Inventory);

                            if (emptySlot >= 0)
                            {
                                Console.WriteLine($"You find a {item} and take it.");
                                player.Inventory[emptySlot] = item;
                            }
                            else
                            {
                                Console.WriteLine($"Your backpack is full! You can't take the {item}.");
                            }
                        }

                        player.CurrentRoom.Items.Clear();
                    }
                }
                else if (choice.StartsWith("drop "))
                {
                    string itemName = choice.Substring(5);
                    bool isRemoved = RemoveItem(player.Inventory, itemName);

                    if (isRemoved == true)
                    {
                        Console.WriteLine($"You drop the {itemName}.");
                        player.CurrentRoom.Items.Add(itemName);
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
                    Console.WriteLine($"Location: {player.CurrentRoom.Name}");
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