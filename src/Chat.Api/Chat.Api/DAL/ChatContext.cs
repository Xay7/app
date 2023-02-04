
using ChatApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.DAL
{
    public class ChatContext : DbContext
    {
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<User> Users => Set<User>();
        public DbSet<RoomUser> RoomsUsers => Set<RoomUser>();
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Message>(message =>
            {
                message.ToTable("Messages");
                message.HasKey(u => u.Id);
                message.Property(u => u.Body);
                message.Property(u => u.Created);
                message.Property(u => u.Updated);
            });


            modelBuilder.Entity<Room>(group =>
            {
                group.ToTable("Rooms");
                group.HasKey(u => u.Id);
                group.Property(u => u.Name);
                group.Property(u => u.Description);
                group.Property(u => u.Created);
                group.Property(u => u.Updated);
                group.HasMany(u => u.Users);
            });

            modelBuilder.Entity<RoomUser>(group =>
            {
                group.ToTable("RoomsUsers");
                group.HasKey(u => u.Id);
                group.Property(u => u.Username);
                group.HasOne(u => u.UserRoom);
            });




            base.OnModelCreating(modelBuilder);
        }
    }
}