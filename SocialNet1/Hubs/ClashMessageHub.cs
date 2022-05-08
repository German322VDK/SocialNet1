using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Models.Hub;
using SocialNet1.Models.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNet1.Hubs
{
    public class ClashMessageHub : Hub
    {
        private string url = "http://localhost:5000/";

        private readonly IGroup _group;
        private readonly IUser _user;
        private readonly ILogger<ClashMessageHub> _logger;
        public ClashMessageHub(IGroup group, IUser user, ILogger<ClashMessageHub> logger)
        {
            _group = group;
            _user = user;
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

            var client = new HttpClient();
            CancellationToken Cancel = default;

            var response = client.PostAsJsonAsync($"{url}{APIUrls.ADD_CLASH_MESSAGE}", message, Cancel).Result.Content.ReadAsStringAsync().Result;

            var responseresult = Convert.ToBoolean(response);

            var user = _user.Get(message.Sender);

            message.RecipientName = $"{user.FirstName}-{user.SecondName}";

            message.Date = DateTime.Now.ToString("t");

            if (responseresult)
                _logger.LogInformation($"Сообщение '{message.Text}' человека '{message.Sender}' в протвостоянии № " +
                    $"({message.ClashId}) успешно сохранено в БД:)");
            else
                _logger.LogWarning($"Сообщение '{message.Text}' человека '{message.Sender}' в протвостоянии № " +
                    $"({message.GroupName}) обломаломь с БД:(");

            await Clients.All.SendAsync("Receive", message);
        }

    }
}
