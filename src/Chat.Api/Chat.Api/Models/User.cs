namespace ChatApi.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        public IReadOnlyCollection<Room> Rooms => rooms.AsReadOnly();
        private readonly List<Room> rooms = new();

        public User() : base()
        {
        }
        public User(string username)
        {
            Username = username;
        }

    }
}
