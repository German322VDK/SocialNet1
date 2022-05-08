using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SocialNet1.Models.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Hubs
{
    public class ClashMessageHub : Hub
    {
        private string url = "http://localhost:5000/";

        private readonly ILogger<ClashMessageHub> _logger;
        public ClashMessageHub(ILogger<ClashMessageHub> logger)
        {
            _logger = logger;
        }

        public async Task Send(ClashMessageModel message)
        {
            if (message is null || string.IsNullOrEmpty(message.Text) || string.IsNullOrEmpty(message.Sender)
                || string.IsNullOrEmpty(message.GroupName))
            {
                _logger.LogWarning("Сообщение не пришло (");

                return;
            }

            message.Date = DateTime.Now.ToString("t");

            await Clients.All.SendAsync("Receive", message);
        }

    }
}
