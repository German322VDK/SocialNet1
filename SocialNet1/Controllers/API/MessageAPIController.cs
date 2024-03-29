﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Models.Hub;

namespace SocialNet1.Controllers.API
{
    [Route("api/message")]
    [ApiController]
    public class MessageAPIController : ControllerBase
    {
        private readonly IChat _chat;
        private readonly IGroupChat _groupChat;
        private readonly IClash _clash;
        private readonly ILogger<MessageAPIController> _logger;

        public MessageAPIController(IChat chat, IGroupChat groupChat, IClash clash, ILogger<MessageAPIController> logger)
        {
            _chat = chat;
            _groupChat = groupChat;
            _clash = clash;
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

        [HttpPost("delete")]
        public bool Delete(SimpMessageDeleteModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.SenderName) || string.IsNullOrEmpty(model.RecipientName))
            {
                _logger.LogWarning($"Не получилось добавить сообщение в бд (");

                return false;
            }

            var result = _chat.DeleteMessage(model.ChatId, model.MessageHelpId);

            if (!result)
            {
                _logger.LogWarning($"Не получилось удалить сообщение № {model.MessageHelpId} человека {model.SenderName} " +
                    $"человеку {model.RecipientName} в бд (");

                return false;
            }

            _logger.LogInformation($"Получилось удалить сообщение № {model.MessageHelpId} человека {model.SenderName} " +
                    $"человеку {model.RecipientName} в бд (");

            return true;
        }

        [HttpPost("update")]
        public bool Update(SimpMessageUpdateModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.SenderName) || string.IsNullOrEmpty(model.RecipientName) 
                || string.IsNullOrEmpty(model.Text))
            {
                _logger.LogWarning($"Не получилось изменить сообщение в бд (");

                return false;
            }

            var result = _chat.UpdateMessage(model.ChatId, model.MessageHelpId, model.Text);

            if (!result)
            {
                _logger.LogWarning($"Не получилось изменить сообщение № {model.MessageHelpId} '{model.Text}' человека {model.SenderName} " +
                    $"человеку {model.RecipientName} в бд (");

                return false;
            }

            _logger.LogInformation($"Получилось изменить сообщение № {model.MessageHelpId} '{model.Text}' человека {model.SenderName} " +
                    $"человеку {model.RecipientName} в бд (");

            return true;
        }


        [HttpPost("addgroup")]
        public bool AddGroup(GroupMessageModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.Content) || string.IsNullOrEmpty(model.SenderName)
                || string.IsNullOrEmpty(model.GroupName))
            {
                _logger.LogWarning($"Не получилось добавить сообщение в бд (");

                return false;
            }

            var result = _groupChat.AddMessage(model.SenderName, model.GroupName, model.Content);

            if (!result)
            {
                _logger.LogWarning($"Не получилось добавить сообщение '{model.Content}' человека {model.SenderName} " +
                    $"человеку {model.RecipientName} в бд (");
            }
            else
            {
                _logger.LogInformation($"Получилось добавить сообщение '{model.Content}' человека {model.SenderName} " +
                    $"человеку {model.RecipientName} в бд )");
            }

            return result;
        }

        [HttpPost("deletegroup")]
        public bool DeleteGroup(GroupMessageDeleteModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.GroupName))
            {
                _logger.LogWarning($"Не получилось удалить групповое сообщение в бд (");

                return false;
            }

            var result = _groupChat.DeleteMessage(model.GroupName, model.MessageHelpId);

            if (!result)
            {
                _logger.LogWarning($"Не получилось удалить сообщение №{model.MessageHelpId} группы '{model.GroupName}' в бд (");
            }
            else
            {
                _logger.LogInformation($"Получилось удалить сообщение №{model.MessageHelpId} группы '{model.GroupName}' в бд ");
            }

            return result;
        }

        [HttpPost("addclash")]
        public bool AddClashMessage(ClashMessageModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.Text) || string.IsNullOrEmpty(model.Sender)
                || string.IsNullOrEmpty(model.GroupName))
            {
                _logger.LogWarning($"Не получилось добавить сообщение в бд (");

                return false;
            }

            var result = _clash.AddMessage(model.ClashId, model.Sender, model.GroupName, model.Text);

            if (!result)
            {
                _logger.LogWarning($"Не получилось добавить сообщение '{model.Text}' человека {model.Sender} " +
                    $"в противостоянии № {model.ClashId} в бд (");
            }
            else
            {
                _logger.LogInformation($"Получилось добавить сообщение '{model.Text}' человека {model.Sender} " +
                    $"в противостоянии № {model.ClashId} в бд )");
            }

            return result;
        }

        
        [HttpPost("setlike")]
        public bool SetLike(ClashLikeModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.Sender))
            {
                _logger.LogWarning($"Не получилось задать лайк в бд (");

                return false;
            }

            if (model.IsAddLike)
            {
                _logger.LogInformation($"Чел {model.Sender} пытается поставить лайк под пост № {model.ClashId} " +
                    $"группы {(model.Is1 ? "1" : "2")}");

                var result = _clash.AddLike(model.ClashId, model.Is1, model.Sender);

                if (result)
                {
                    _logger.LogInformation($"Получилось челу {model.Sender} поставить лайк под пост № {model.ClashId} " +
                    $"группы {(model.Is1 ? "1" : "2")}");
                }
                else
                {
                    _logger.LogWarning($"Не получилось челу {model.Sender} поставить лайк под пост № {model.ClashId} " +
                    $"группы {(model.Is1 ? "1" : "2")}");
                }

                return result;
            }
            else
            {
                _logger.LogInformation($"Чел {model.Sender} пытается убрать лайк под пост № {model.ClashId} " +
                    $"группы {(model.Is1 ? "1" : "2")}");

                var result = _clash.DeleteLike(model.ClashId, model.Is1, model.Sender);

                if (result)
                {
                    _logger.LogInformation($"Получилось челу {model.Sender} убрать лайк под пост № {model.ClashId} " +
                    $"группы {(model.Is1 ? "1" : "2")}");
                }
                else
                {
                    _logger.LogWarning($"Не получилось челу {model.Sender} убрать лайк под пост № {model.ClashId} " +
                    $"группы {(model.Is1 ? "1" : "2")}");
                }

                return result;
            }

        }
    }
}
