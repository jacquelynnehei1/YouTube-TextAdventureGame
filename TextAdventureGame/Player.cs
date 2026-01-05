namespace TextAdventureGame
{
    public class Player
    {
        public int Health;
        public int MaxHealth;
        public int Gold;
        public string[] Inventory;
        public Room CurrentRoom;

        public Player(int startingHealth, int startingGold, Room startingRoom)
        {
            Health = startingHealth;
            MaxHealth = startingHealth;
            Gold = startingGold;
            CurrentRoom = startingRoom;
            Inventory = new string[10];
        }

        public void Heal(int amount)
        {
            Health += amount;

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
        }

        public void AddGold(int amount)
        {
            Gold += amount;
        }
    }
}