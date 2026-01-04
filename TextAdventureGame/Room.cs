namespace TextAdventureGame
{
    public class Room
    {
        public string Name;
        public string Description;
        public List<string> Items = new List<string>();
        public Room Exit;

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}