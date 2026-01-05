namespace TextAdventureGame
{
    public class Game
    {
        private Player player;
        private Room camp;
        private Room cave;
        private Room forrest;
        private bool isPlaying;
        private Random random;

        public Game()
        {
            random = new Random();

            camp = new Room("Camp", "A warm fire crackles nearby. You feel safe here.");
            cave = new Room("Cave", "A dark, echoing space. Water drips from the ceiling.");
            forrest = new Room("Forrest", "Tall trees surround you. You get the feeling of being watched.");

            camp.Items.Add("health potion");
            camp.Items.Add("shield");
            cave.Items.Add("rusty key");
            forrest.Items.Add("sword");

            cave.Enemy = new Enemy("Goblin", 8, 2);
            forrest.Enemy = new Enemy("Wolf", 6, 3);

            forrest.Exits.Add("south", camp);
            camp.Exits.Add("north", forrest);
            camp.Exits.Add("south", cave);
            cave.Exits.Add("north", camp);

            player = new Player(10, 0, camp);

            isPlaying = true;
        }

        private void DisplayInventory(string[] inventory)
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

        private bool RemoveItem(string[] inventory, string itemName)
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

        private int FindEmptySlot(string[] inventory)
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

        private bool HasItem(string[] inventory, string itemName)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (player.Inventory[i] == itemName)
                {
                    return true;
                }
            }

            return false;
        }

        private bool AddItem(string[] inventory, string itemName)
        {
            int emptySlot = FindEmptySlot(inventory);

            if (emptySlot >= 0)
            {
                inventory[emptySlot] = itemName;
                return true;
            }

            return false;
        }

        public void Run()
        {
            Console.WriteLine("Hello, adventurer!");

            Console.WriteLine("What is your name?");
            string playerName = Console.ReadLine();

            Console.WriteLine($"Greetings, {playerName}! Your adventure begins...");

            while (isPlaying)
            {
                Console.WriteLine(player.CurrentRoom.Description);
                Console.WriteLine("What would you like to do?");

                string choice = Console.ReadLine().ToLower().Trim();

                if (choice == "look")
                {
                    foreach (KeyValuePair<string, Room> exit in player.CurrentRoom.Exits)
                    {
                        Console.WriteLine($"You see an exit to the {exit.Key} leading to a {exit.Value.Name}.");    
                    }

                    if (player.CurrentRoom.Enemy != null)
                    {
                        Console.WriteLine($"You also see a {player.CurrentRoom.Enemy.Name}. Prepare for an attack!");
                    }
                }
                else if (choice.StartsWith("move "))
                {
                    string direction = choice.Substring(5);

                    if (player.CurrentRoom.Exits.ContainsKey(direction))
                    {
                        player.CurrentRoom = player.CurrentRoom.Exits[direction];
                        Console.WriteLine($"You travel {direction} to {player.CurrentRoom.Name}.");
                    }
                    else
                    {
                        Console.WriteLine($"You can't go {direction} from here.");
                    }
                }
                else if (choice == "inventory")
                {
                    DisplayInventory(player.Inventory);
                }
                else if (choice.StartsWith("take "))
                {
                    string itemName = choice.Substring(5);
                    
                    if (itemName == "all")
                    {
                        foreach (string item in player.CurrentRoom.Items)
                        {
                            if (AddItem(player.Inventory, item))
                            {
                                Console.WriteLine($"You take the {item}.");
                            }
                            else
                            {
                                Console.WriteLine($"You can't take the {item}. Your backpack is full!");
                            }
                        }
                    }
                    else if (player.CurrentRoom.Items.Contains(itemName))
                    {
                        if (AddItem(player.Inventory, itemName))
                        {
                            Console.WriteLine($"You take the {itemName}.");
                        }
                        else
                        {
                            Console.WriteLine($"You can't take the {itemName}. Your backpack is full!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"There is no {itemName} to take.");
                    }
                }
                else if (choice == "search")
                {
                    if (player.CurrentRoom.Items.Count == 0)
                    {
                        Console.WriteLine("You search but find nothing.");
                    }
                    else
                    {
                        string searchResult = "You find: ";

                        foreach (string item in player.CurrentRoom.Items)
                        {
                            searchResult += $"{item}, ";    
                        }

                        searchResult = searchResult.Substring(0, searchResult.Length - 2);
                        Console.WriteLine(searchResult);
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
                else if (choice == "attack")
                {
                    if (player.CurrentRoom.Enemy != null && player.CurrentRoom.Enemy.Health > 0)
                    {
                        Enemy enemy = player.CurrentRoom.Enemy;

                        int playerRoll = random.Next(1, 7);
                        int attackBonus = 0;

                        if (HasItem(player.Inventory, "sword"))
                        {
                            attackBonus += 2;
                            Console.WriteLine($"You attack the {enemy.Name} with your sword!");
                        }
                        else
                        {
                            Console.WriteLine($"You attack the {enemy.Name}!");
                        }

                        Console.WriteLine($"You rolled a {playerRoll} + {attackBonus} = {playerRoll + attackBonus} damage.");

                        enemy.TakeDamage(playerRoll + attackBonus);

                        if (enemy.Health <= 0)
                        {
                            Console.WriteLine($"You killed the {enemy.Name}! The {enemy.Name} drops 5 gold.");
                            player.AddGold(5);
                        }
                        else
                        {
                            int enemyRoll = random.Next(1, 7);
                            int enemyDamage = enemyRoll + enemy.Attack;

                            Console.WriteLine($"The {enemy.Name} attacks back!");

                            if (HasItem(player.Inventory, "shield"))
                            {
                                enemyDamage -= 2;

                                if (enemyDamage < 0)
                                {
                                    enemyDamage = 0;
                                }

                                Console.WriteLine("Your shield blocks 2 damage.");
                            }

                            Console.WriteLine($"It rolled a {enemyRoll} + {enemy.Attack} = {enemyDamage} damage.");

                            player.TakeDamage(enemyDamage);
                            Console.WriteLine($"You now have {player.Health} health.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There's nothing to attack here");
                    }
                }
                else if (choice.StartsWith("use "))
                {
                    string itemName = choice.Substring(4);

                    if (HasItem(player.Inventory, itemName) == false)
                    {
                        Console.WriteLine($"You don't have a {itemName}.");
                    }
                    else if (itemName == "health potion")
                    {
                        RemoveItem(player.Inventory, itemName);
                        player.Heal(5);
                        Console.WriteLine("You drink the health potion and restore 5 health!");
                        Console.WriteLine($"You now have {player.Health}/{player.MaxHealth} health.");
                    }
                    else
                    {
                        Console.WriteLine($"You can't use that {itemName}");
                    }
                }
                else if (choice == "stats")
                {
                    Console.WriteLine($"Health: {player.Health}");
                    Console.WriteLine($"Gold: {player.Gold}");
                    Console.WriteLine($"Location: {player.CurrentRoom.Name}");
                }
                else if (choice == "help")
                {
                    Console.WriteLine("\nAvailable commands:");
                    Console.WriteLine("  look - Look around the room to see where you can move.");
                    Console.WriteLine("  move [direction] - Move to another room (e.g., 'move north')");
                    Console.WriteLine("  attack - Attack an enemy in the room");
                    Console.WriteLine("  use [item] - Use an item (e.g., 'use health potion')");
                    Console.WriteLine("  search - Search the current room for items");
                    Console.WriteLine("  inventory - View your inventory");
                    Console.WriteLine("  drop [item] - Drop an item (e.g., 'drop rusty key')");
                    Console.WriteLine("  stats - View your health, gold, and location");
                    Console.WriteLine("  help - Show this help message");
                    Console.WriteLine("  quit - Exit the game");
                }
                else if (choice == "quit")
                {
                    isPlaying = false;
                    Console.WriteLine("Your adventure has come to an end...");
                }
                else
                {
                    Console.WriteLine("I don't understand that. Type 'help' for a list of commands.");
                }

                if (cave.Enemy.Health <= 0 && forrest.Enemy.Health <= 0)
                {
                    Console.WriteLine("\n=== VICTORY! ===");
                    Console.WriteLine("You have defeated all of the enemies!");
                    Console.WriteLine($"You collected {player.Gold} gold on your journey.");
                    Console.WriteLine("\nYou are victorious, brave adventurer!");
                    isPlaying = false;
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