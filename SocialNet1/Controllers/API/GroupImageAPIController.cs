﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.Models.API;
using System.Linq;

namespace SocialNet1.Controllers.API
{
    [Route("api/groupimage")]
    [ApiController]
    public class GroupImageAPIController : ControllerBase
    {
        private readonly ILogger<GroupImageAPIController> _logger;
        private readonly IGroup _group;
        private readonly IUser _user;

        public GroupImageAPIController(ILogger<GroupImageAPIController> logger, IGroup group, IUser user)
        {
            _logger = logger;
            _group = group;
            _user = user;
        }

        [HttpPost("delete")]
        public bool DeleteImage(DeleteGroupImageModel model)
        {
            if (string.IsNullOrEmpty(model.GroupName))
            {
                _logger.LogWarning($"Не понятно какой группе удалять фото");

                return false;
            }

            if (_group.Get(model.GroupName) is null)
            {
                _logger.LogWarning($"Не существует группы {model.GroupName}");

                return false;
            }

            var result = _group.DeletePhoto(model.GroupName, model.ImageId);

            if (result)
            {
                _logger.LogInformation($"Получилось удалить фото {model.ImageId} группы {model.GroupName}");
            }
            else
            {
                _logger.LogWarning($"Не получилось удалить фото {model.ImageId} группы {model.GroupName}");
            }

            return result;
        }

        [HttpGet("addlike")]
        public bool AddImageLike(string groupname, string username, int imageid)
        {
            if(string.IsNullOrEmpty(groupname) || string.IsNullOrEmpty(groupname))
            {
                _logger.LogWarning($"Не понятно кто кому ставит лайк");

                return false;
            }

            if(_user.Get(username) is null || _group.Get(groupname) is null)
            {
                _logger.LogWarning($"Не существует группы {groupname} или человека {username}");

                return false;
            }

            var result = _group.AddPhotoLike(groupname, username, imageid);

            if (result)
            {
                _logger.LogInformation($"Получилось добавить лайк под фото {imageid} группы {groupname} человеку {username}");
            }
            else
            {
                _logger.LogWarning($"Не получилось добавить лайк под фото {imageid} группы {groupname} человеку {username}");
            }

            return result;
        }

        [HttpGet("deletelike")]
        public bool DeleteImageLike(string groupname, string username, int imageid)
        {
            if (string.IsNullOrEmpty(groupname) || string.IsNullOrEmpty(groupname))
            {
                _logger.LogWarning($"Не понятно кто кому убирает лайк");

                return false;
            }

            if (_user.Get(username) is null || _group.Get(groupname) is null)
            {
                _logger.LogWarning($"Не существует группы {groupname} или человека {username}");

                return false;
            }

            var result = _group.DeletePhotoLike(groupname, username, imageid);

            if (result)
            {
                _logger.LogInformation($"Получилось убрать лайк под фото {imageid} группы {groupname} человеку {username}");
            }
            else
            {
                _logger.LogWarning($"Не убрать добавить лайк под фото {imageid} группы {groupname} человеку {username}");
            }

            return result;
        }

        [HttpPost("addcom")]
        public PhotoGroupInfoComms AddCom(AddGroupCommentImageModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.GroupName) || string.IsNullOrEmpty(model.Text)
                || string.IsNullOrEmpty(model.Sender))
            {
                _logger.LogWarning($"Не получилось добавить комментарий, данные не пришли(");

                return null;
            }

            string groupName = model.GroupName, text = model.Text, sender = model.Sender;

            int imageId = model.ImageId;

            var user = _user.Get(sender);

            var group = _group.Get(groupName);

            if(user is null || group is null)
            {
                _logger.LogWarning($"Группы {groupName} или человека {sender} не существует(");

                return null;
            }

            var com = _group.AddCommentToPhoto(groupName, sender, text, imageId);

            _logger.LogInformation($"{sender} пытается добавить комментарий: {text} под фото {imageId} принадлежащее {groupName}");

            if (com == null)
            {
                _logger.LogWarning($"{sender} не смог добавить комментарий: {text} под фото {imageId} принадлежащее {groupName}");

                return null;
            }

            var senderUser = _user.Get(sender);

            var curImageArr = senderUser.Images.SingleOrDefault(el => el.ImageNumber == senderUser.SocNetItems.CurrentImage).Image;

            var format = NewImageMethods.GetFormat(curImageArr);

            var curImage = NewImageMethods.GetStringFromByteArr(curImageArr);

            _logger.LogInformation($"{sender} смог добавить комментарий: {text} под фото {imageId} принадлежащее {groupName}");

            return new PhotoGroupInfoComms 
            {
                DateTime = com.DateTime.ToString("g"),
                Comment = com.Text,
                //Likes = com.LikeCount,
                AuthorFirstName = senderUser.FirstName,
                AuthorSecondName = senderUser.SecondName,
                AuthorUserName = senderUser.UserName,
                AuthorCoordinatesImage = $"photo/coordinates/{senderUser.SocNetItems.X}d{senderUser.SocNetItems.Y}.jpg",
                AuthorImage = curImage,
                AuthorFormat = format,
                HelpId = com.HelpId
            };
        }

        [HttpGet("deletecom")]
        public bool DeleteCom(string groupName, int imageId, int comId)
        {
            if(string.IsNullOrEmpty(groupName) || _group.Get(groupName) is null)
            {
                _logger.LogWarning("Не понятно кому удалять коммент под фото");

                return false;
            }

            var result = _group.DeletePhotoCom(groupName, imageId, comId);

            if (result)
            {
                _logger.LogInformation($"Получилось удалить коммент {comId} под фото {imageId} группы {groupName}");
            }
            else
            {
                _logger.LogWarning($"Не получилось удалить коммент {comId} под фото {imageId} группы {groupName}");
            }

            return result;
        }

        [HttpPost("addlikecom")]
        public bool AddLikeCom(LikeGroupComModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.GroupName) || string.IsNullOrEmpty(model.UserName)
                || _group.Get(model.GroupName) is null)
            {
                _logger.LogWarning($"{model.UserName ?? ""} не смог поставить лайк {model.GroupName ?? ""}!");
                return false;
            }

            var result = _group.AddLikeComPhoto(model.GroupName, model.UserName, model.ImageId, model.ComId);

            if (!result)
            {
                _logger.LogWarning($"{model.UserName} уже до этого поставил лайк {model.GroupName} под коммент {model.ComId} фото {model.ImageId}!");
            }
            else
            {
                _logger.LogInformation($"{model.UserName} поставил лайк {model.GroupName} под коммент {model.ComId} фото {model.ImageId}!");
            }

            return result;

        }

        [HttpPost("deletelikecom")]
        public bool DeleteLikeCom(LikeGroupComModel model)
        {
            if (model is null || string.IsNullOrEmpty(model.GroupName) || string.IsNullOrEmpty(model.UserName)
                || _group.Get(model.GroupName) is null)
            {
                _logger.LogWarning($"{model.UserName ?? ""} не смог поставить лайк {model.GroupName ?? ""}!");
                return false;
            }

            var result = _group.DeleteLikeComPhoto(model.GroupName, model.UserName, model.ImageId, model.ComId);

            if (!result)
            {
                _logger.LogWarning($"{model.UserName} ещё не поставил лайк {model.GroupName} под коммент {model.ComId} фото {model.ImageId}!");
            }
            else
            {
                _logger.LogInformation($"{model.UserName} убрал лайк {model.GroupName} под коммент {model.ComId} фото {model.ImageId}!");
            }

            return result;
        }
    }
}
