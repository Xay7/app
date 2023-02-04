namespace ChatApi.Models
{
    public class RoomUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;

        public Room UserRoom { get; set; } = new();


        public RoomUser() : base()
        {
        }

        public RoomUser(Room room, string user)
        {
            this.UserRoom = room;
            this.Username = user;
        }

    }
}
