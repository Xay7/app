using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        public Message(string body, string username)
        {
            Body = body;
            Username = username;
        }
    }
}