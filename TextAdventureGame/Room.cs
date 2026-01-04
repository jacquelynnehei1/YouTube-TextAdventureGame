namespace TextAdventureGame
{
    public class Room
    {
        public string Name;
        public string Description;
        public List<string> Items = new List<string>();
        public Dictionary<string, Room> Exits = new Dictionary<string, Room>();
        public Enemy Enemy;

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}