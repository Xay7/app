namespace ChatApi.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        public ICollection<User> Users => users;
        private readonly List<User> users = new();

        public ICollection<Message> Messages => messages;
        private readonly List<Message> messages = new();
        public Room() : base()
        {
        }
        public Room(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
