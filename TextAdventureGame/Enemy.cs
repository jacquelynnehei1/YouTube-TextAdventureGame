namespace TextAdventureGame
{
    public class Enemy
    {
        public string Name;
        public int Health;
        public int Attack;

        public Enemy(string name, int health, int attack)
        {
            Name = name;
            Health = health;
            Attack = attack;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
        }
    }
}