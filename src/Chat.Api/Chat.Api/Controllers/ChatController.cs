using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ChatApi.Models;
using ChatApi.Hubs;
using ChatApi.DAL;
using Microsoft.EntityFrameworkCore;
using ChatApi.Interfaces;
using System.Text.RegularExpressions;

namespace Public_Chat.Controllers
{
    [Route("api")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private ChatContext context;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IKeycloakUserContext _keycloakUserContext;

        public ChatController(ChatContext c, IHubContext<ChatHub> hubContext, IKeycloakUserContext keycloakUserContext)
        {
            _hubContext = hubContext;
            context = c;
            _keycloakUserContext = keycloakUserContext;
        }

        [Route("rooms")] 
        [HttpGet]
        public async Task<List<Room>> GetChatrooms([FromQuery] GetRoomsRequest req)
        {
            var rooms = await context.RoomsUsers.Where(o => o.Username == req.Username).Select(o => o.UserRoom).ToListAsync();

            return rooms;
        }

        [Route("rooms")]
        [HttpPost]
        public async Task<string> CreateRoom([FromBody] CreateRoomRequest req)
        {
            var rooms = context.Rooms.ToList();

            var user = await _keycloakUserContext.FindUserByLogin(req.Username);

            if (user == null)
            {
                return "Error";
            }

            var chat = new Room(req.Name, req.Description);
            var roomUser = new RoomUser(chat, req.Username);


            var res = await context.Rooms.AddAsync(chat);
            await context.RoomsUsers.AddAsync(roomUser);
            await context.SaveChangesAsync();

            return "Dodano";
        }

        [Route("rooms")]
        [HttpDelete]
        public async Task<string> DeleteRoom([FromBody] DeleteRoomRequest req)
        {
            var rooms = context.Rooms.ToList();

            var user = await _keycloakUserContext.FindUserByLogin(req.Username);

            if (user == null)
            {
                return "Error";
            }

            var room = await context.Rooms.Where(o => o.Id == req.Id).FirstOrDefaultAsync();
            var roomUser = await context.RoomsUsers.Where(o => o.Id == req.Id).ToListAsync();

            context.Rooms.Remove(room);
            context.RoomsUsers.RemoveRange(roomUser);

            await context.SaveChangesAsync();

            return "Usuniêto";
        }

        [Route("rooms/users")]
        [HttpPut]
        public async Task<string> AddUserToRoom([FromBody] AddUserToRoomRequest req)
        {
            var room = await context.Rooms.Where(o => o.Id == req.RoomId).FirstOrDefaultAsync();

            var user = await _keycloakUserContext.FindUserByLogin(req.Username);

            if (user == null)
            {
                return "Error";
            }

            var u = new User(req.Username);
            var b = new RoomUser(room, req.Username);
            context.RoomsUsers.Add(b);

            await context.SaveChangesAsync();
     

            return "Dodano u¿ytkownika";
        }

        [Route("rooms/messages")]
        [HttpGet]
        public async Task<List<Message>> GetMessages([FromQuery] GetRoomsMessagesRequest req)
        {
            var messages = await context.Rooms.Where(o => o.Id == req.RoomId).Select(o => o.Messages.ToList()).FirstOrDefaultAsync();

            return messages;
        }

        [Route("rooms/messages")]
        [HttpPost]
        public async Task<string> AddMessage([FromBody] PostRoomsMessageRequest req)
        {

            var room = await context.Rooms.Where(o => o.Id == req.RoomId).FirstOrDefaultAsync();
            var message = new Message(req.Text, req.Username);
            room.Messages.Add(message);

            await context.SaveChangesAsync();

            return "Dodano";
        }


    }
}

public class AddUserToRoomRequest
{
    public int RoomId { get; set; }
    public string Username { get; set; }
}

    public class PostRoomsMessageRequest
{
    public int RoomId { get; set; }
    public string Username { get; set; }
    public string Text { get; set; }
}


public class GetRoomsMessagesRequest
{
    public int RoomId { get; set; }
}

public class GetRoomsRequest
{
    public string Username { get; set; }
}


public class CreateRoomRequest
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class DeleteRoomRequest
{
    public string Username { get; set; }
    public int Id { get; set; }
}