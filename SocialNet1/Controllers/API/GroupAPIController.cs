using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Controllers.API
{
    [Route("api/group")]
    [ApiController]
    public class GroupAPIController : ControllerBase
    {
        private readonly ILogger<GroupAPIController> _logger;
        private readonly IGroup _group;
        private readonly IUser _user;

        public GroupAPIController(ILogger<GroupAPIController> logger, IGroup group, IUser user)
        {
            _logger = logger;
            _group = group;
            _user = user;
        }

        [HttpGet("deletepost")]
        public bool DeletePost(string groupName, int postId)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                _logger.LogWarning("Не понятно кому удалять пост");

                return false;
            }

            var result = _group.DeletePost(groupName, postId);

            if (!result)
            {
                _logger.LogInformation($"На странице группы {groupName} не получилось удалить пост № {postId}");
            }
            else
            {
                _logger.LogInformation($"На странице группы {groupName} был удалён пост № {postId}");
            }

            return result;
        }

        //[HttpPost("delete")]
        //public bool DeleteImage(DeleteGroupImageModel model)
        //{
        //    if (string.IsNullOrEmpty(model.GroupName))
        //    {
        //        _logger.LogWarning($"Не понятно какой группе удалять фото");

        //        return false;
        //    }

        //    if (_group.Get(model.GroupName) is null)
        //    {
        //        _logger.LogWarning($"Не существует группы {model.GroupName}");

        //        return false;
        //    }

        //    var result = _group.DeletePhoto(model.GroupName, model.ImageId);

        //    if (result)
        //    {
        //        _logger.LogInformation($"Получилось удалить фото {model.ImageId} группы {model.GroupName}");
        //    }
        //    else
        //    {
        //        _logger.LogWarning($"Не получилось удалить фото {model.ImageId} группы {model.GroupName}");
        //    }

        //    return result;
        //}

        //[HttpGet("addlike")]
        //public bool AddImageLike(string groupname, string username, int imageid)
        //{
        //    if (string.IsNullOrEmpty(groupname) || string.IsNullOrEmpty(groupname))
        //    {
        //        _logger.LogWarning($"Не понятно кто кому ставит лайк");

        //        return false;
        //    }

        //    if (_user.Get(username) is null || _group.Get(groupname) is null)
        //    {
        //        _logger.LogWarning($"Не существует группы {groupname} или человека {username}");

        //        return false;
        //    }

        //    var result = _group.AddPhotoLike(groupname, username, imageid);

        //    if (result)
        //    {
        //        _logger.LogInformation($"Получилось добавить лайк под фото {imageid} группы {groupname} человеку {username}");
        //    }
        //    else
        //    {
        //        _logger.LogWarning($"Не получилось добавить лайк под фото {imageid} группы {groupname} человеку {username}");
        //    }

        //    return result;
        //}
    }
}
