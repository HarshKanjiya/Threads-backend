﻿using Microsoft.AspNetCore.SignalR;

namespace UserApi.microservice.Hubs
{
    public class UserHub : Hub
    {
        public async Task SendUpdate(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
