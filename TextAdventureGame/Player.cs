namespace TextAdventureGame
{
    public class Player
    {
        public int Health;
        public int Gold;
        public string[] Inventory;
        public Location CurrentLocation;

        public Player(int startingHealth, int startingGold, Location startingLocation)
        {
            Health = startingHealth;
            Gold = startingGold;
            CurrentLocation = startingLocation;
            Inventory = new string[10];
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