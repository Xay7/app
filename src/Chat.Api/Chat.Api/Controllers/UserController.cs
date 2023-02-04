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
using ChatApi.Interfaces;

namespace Public_Chat.Controllers
{
    [Route("chat")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IKeycloakUserContext keycloakUserContext;
        private readonly ChatContext chatContext;

        public UserController(IHubContext<ChatHub> hubContext, IKeycloakUserContext keycloakUserContext, ChatContext chatContext)
        {
            this._hubContext = hubContext;
            this.keycloakUserContext = keycloakUserContext;
            this.chatContext = chatContext; 
        }

    }
}