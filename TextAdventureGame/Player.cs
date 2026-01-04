namespace TextAdventureGame
{
    public class Player
    {
        public int Health;
        public int Gold;
        public string[] Inventory;
        public Room CurrentRoom;

        public Player(int startingHealth, int startingGold, Room startingRoom)
        {
            Health = startingHealth;
            Gold = startingGold;
            CurrentRoom = startingRoom;
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