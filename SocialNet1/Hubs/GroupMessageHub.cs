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
    public class GroupMessageHub : Hub
    {
        private string url = "http://localhost:5000/";

        private readonly ILogger<GroupMessageHub> _logger;
        private readonly IGroup _group;
        private readonly IUser _user;
        public GroupMessageHub(IGroup group, IUser user, ILogger<GroupMessageHub> logger)
        {
            _group = group;
            _user = user;
            _logger = logger;
        }

        public async Task Send(GroupMessageModel message)
        {
            if (message is null || string.IsNullOrEmpty(message.Content) || string.IsNullOrEmpty(message.SenderName)
                || string.IsNullOrEmpty(message.GroupName))
            {
                _logger.LogWarning("Сообщение не пришло (");

                return;
            }

            message.Date = DateTime.Now.ToString("t");

            var client = new HttpClient();
            CancellationToken Cancel = default;

            var response = client.PostAsJsonAsync($"{url}{APIUrls.ADD_GROUP_MESSAGE}", message, Cancel).Result.Content.ReadAsStringAsync().Result;

            var responseresult = Convert.ToBoolean(response);

            //var responseresult = true;

            var groupSubs = _group.Get(message.GroupName).Users.Select(us => us.UserName);

            var user = _user.Get(message.SenderName);

            message.RecipientName = $"{user.FirstName}-{user.SecondName}";

            if (responseresult)
                _logger.LogInformation($"Сообщение '{message.Content}' человека '{message.SenderName}' и группы" +
                    $"({message.GroupName}) успешно сохранено в БД:)");
            else
                _logger.LogWarning($"Сообщение '{message.Content}' человека '{message.SenderName}' и группы" +
                    $"({message.GroupName}) обломаломь с БД:(");

            await Clients.Users(groupSubs).SendAsync("Receive", message);
        }

    }
}
