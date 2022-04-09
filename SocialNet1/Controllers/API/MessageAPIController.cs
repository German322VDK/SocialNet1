using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Models.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Controllers.API
{
    [Route("api/message")]
    [ApiController]
    public class MessageAPIController : ControllerBase
    {
        private readonly IChat _chat;
        private readonly ILogger<MessageAPIController> _logger;

        public MessageAPIController(IChat chat, ILogger<MessageAPIController> logger)
        {
            _chat = chat;
            _logger = logger;
        }

        [HttpPost("add")]
        public int Add(SimpMessageModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.Content) || string.IsNullOrEmpty(model.SenderName)
                || string.IsNullOrEmpty(model.RecipientName))
            {
                _logger.LogWarning($"Не получилось добавить сообщение в бд (");

                return 0;
            }

            var result = _chat.AddMessage(model.SenderName, model.RecipientName, model.Id, model.Content);

            if (result == 0)
            {
                _logger.LogWarning($"Не получилось добавить сообщение '{model.Content}' человека {model.SenderName} " +
                    $"человеку {model.RecipientName} в бд (");

                return 0;
            }

            _logger.LogInformation($"Получилось добавить сообщение '{model.Content}' человека {model.SenderName} " +
                    $"человеку {model.RecipientName} в бд )");

            return result;
        }
    }
}
