using ChatApi.DAL;
using ChatApi.Interfaces;
using ChatApi.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using static ChatApi.DAL.KeycloakUserContext;

namespace ChatApi.Hubs
{
    public class ChatHub : Hub
    {
        private ChatContext context;
        private readonly IKeycloakUserContext keycloakUserContext;

        public ChatHub(ChatContext context, IKeycloakUserContext keycloakUserContext)
        {
            this.keycloakUserContext = keycloakUserContext;
            this.context = context;
        }

        public override async Task<Task> OnConnectedAsync()
        {
            var ctx = Context.GetHttpContext().User;
            var rooms = await context.RoomsUsers.Include(p => p.UserRoom).Where(o => o.Username == ctx.Identity.Name).ToListAsync();

            foreach (var room in rooms)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, room.UserRoom.Id.ToString());
            }


            return Task.CompletedTask;
        }


        public async Task JoinChatroom(string roomId)
        {
            var room = await context.Rooms.Where(o => o.Id == int.Parse(roomId)).FirstOrDefaultAsync();
            if (room == null)
            {
                return;
            }
            var roomUser = new RoomUser(room, Context.GetHttpContext().User.Identity.Name);
            context.RoomsUsers.Add(roomUser);
            await context.SaveChangesAsync();


            await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());
        }

        public async Task AddChatroom(string name, string description, string username)
        {
            var user = await keycloakUserContext.FindUserByLogin(username);

            if (user == null) return;


            Room room = new Room(name, description);
            var roomUser = new RoomUser(room, username);


            var r = await context.Rooms.AddAsync(room);

            await context.RoomsUsers.AddAsync(roomUser);
            await context.SaveChangesAsync();

            await Groups.AddToGroupAsync(Context.ConnectionId, r.Entity.Id.ToString());
        }

        public async Task SendMessage(string username, string message, string roomId)
        {
            var room = await context.Rooms.Where(o => o.Id == int.Parse(roomId)).FirstOrDefaultAsync();
            var msg = new Message(message, username);
            room.Messages.Add(msg);
            await context.SaveChangesAsync();
            await Clients.Group(roomId).SendAsync("GetMessage", new {room, msg } );
        }

        public Task SendPrivateMessage(string user, string message)
        {

            return Clients.User(user).SendAsync("ReceiveMessage", message);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }

    }
    public class JoinChatroomRequest
    {
        public string RoomId { get; set; }
    }

    public class AddChatroomRequest {
        public string username { get; set; }
        public string name { get; set; }
        public string description{ get; set; }
}

}

