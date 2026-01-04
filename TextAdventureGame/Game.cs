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

        public void Run()
        {
            Console.WriteLine("Hello, adventurer!");

            Console.WriteLine("What is your name?");
            string playerName = Console.ReadLine();

            Console.WriteLine($"Greetings, {playerName}! Your adventure begins...");

            while (isPlaying)
            {
                Console.WriteLine(player.CurrentRoom.Description);

                if (player.CurrentRoom.Enemy != null && player.CurrentRoom.Enemy.Health > 0)
                {
                    Console.WriteLine($"A {player.CurrentRoom.Enemy.Name} is here! (Health: {player.CurrentRoom.Enemy.Health})");
                }

                Console.Write("Exits: ");
                foreach (KeyValuePair<string, Room> exit in player.CurrentRoom.Exits)
                {
                    Console.Write($"{exit.Key} ");
                }

                Console.WriteLine();

                string choice = Console.ReadLine().ToLower();

                if (choice.StartsWith("move "))
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
                else if (choice == "attack")
                {
                    if (player.CurrentRoom.Enemy != null && player.CurrentRoom.Enemy.Health > 0)
                    {
                        Enemy enemy = player.CurrentRoom.Enemy;

                        int playerRoll = random.Next(1, 7);
                        Console.WriteLine($"You attack the {enemy.Name}!");
                        Console.WriteLine($"You rolled a {playerRoll} and deal {playerRoll} damage.");

                        enemy.Health -= playerRoll;

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
                else if (choice == "stats")
                {
                    Console.WriteLine($"Health: {player.Health}");
                    Console.WriteLine($"Gold: {player.Gold}");
                    Console.WriteLine($"Location: {player.CurrentRoom.Name}");
                }
                else if (choice == "help")
                {
                    Console.WriteLine("\nAvailable commands:");
                    Console.WriteLine("  move [direction] - Move to another room (e.g., 'move north')");
                    Console.WriteLine("  attack - Attack an enemy in the room");
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

                if (player.Health <= 0)
                {
                    Console.WriteLine("You collapse from exhaustion. Game over.");
                    isPlaying = false;
                }
            }
        }
    }
}