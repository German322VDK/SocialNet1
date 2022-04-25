﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}