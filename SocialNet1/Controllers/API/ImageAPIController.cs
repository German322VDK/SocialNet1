﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.Models;
using SocialNet1.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Controllers.API
{
    [Route("api/image")]
    [ApiController]
    public class ImageAPIController : ControllerBase
    {
        private IUser _user;
        private readonly ILogger<ImageAPIController> _logger;

        public ImageAPIController(IUser user, ILogger<ImageAPIController> logger)
        {
            _user = user;
            _logger = logger;
        }

        //[HttpPost]
        //public void Add([FromBody] AddCommentImageModel data)
        //{

        //}

        [HttpGet("add")]
        public PhotoUserInfo Add(string text, string sender, string recipient, int imageId)
        {
            var com = _user.AddCommentToPhoto(recipient, sender, text, imageId);

            _logger.LogInformation($"{sender} пытается добавить комментарий: {text} под фото {imageId} принадлежащее {recipient}");

            if (com == null)
            {
                _logger.LogInformation($"{sender} не смог добавить комментарий: {text} под фото {imageId} принадлежащее {recipient}");

                return null;
            }
                
            var senderUser = _user.Get(sender);

            var curImageArr = senderUser.Images.SingleOrDefault(el => el.ImageNumber == senderUser.SocNetItems.CurrentImage).Image;

            var format = ImageMethods.GetFormat(curImageArr);

            var curImage = ImageMethods.GetStringFromByteArr(curImageArr);

            _logger.LogInformation($"{sender} смог добавить комментарий: {text} под фото {imageId} принадлежащее {recipient}");

            return new PhotoUserInfo
            {
                DateTime = com.DateTime.ToString("D"),
                Comment = com.Text,
                //Likes = com.LikeCount,
                AuthorFirstName = senderUser.FirstName,
                AuthorSecondName = senderUser.SecondName,
                AuthorUserName = senderUser.UserName,
                AuthorCoordinatesImage = $"photo/coordinates/{senderUser.SocNetItems.X}d{senderUser.SocNetItems.Y}.jpg",
                AuthorImage = curImage,
                AuthorFormat = format
            };
        }

        [HttpGet("addlike")]
        public bool AddLike(string username1, string username2, int imageid)
        {
            if (!(username1 is not null && username2 is not null))
            {
                _logger.LogInformation($"{username1 ?? ""} не смог поставить лайк {username2 ?? ""}!");
                return false;
            }

            var result = _user.AddLikePhoto(username1, username2, imageid);

            if (!result)
            {
                _logger.LogInformation($"{username1} уже до этого поставил лайк {username2} под фото {imageid}!");
            }
            else
            {
                _logger.LogInformation($"{username1} поставил лайк {username2} под фото {imageid}!");
            }

            return result;
        }


        [HttpGet("deletelike")]
        public bool DeleteLike(string username1, string username2, int imageid)
        {
            if (!(username1 is not null && username2 is not null))
            {
                _logger.LogInformation($"{username1 ?? ""} не смог убрать лайк {username2 ?? ""}!");
                return false;
            }

            var result = _user.DeleteLikePhoto(username1, username2, imageid);

            if (!result)
            {
                _logger.LogInformation($"{username1} ещё не ставил лайк {username2} под фото {imageid}!");
            }
            else
            {
                _logger.LogInformation($"{username1} убрал лайк {username2} под фото {imageid}!");
            }

            return result;
        }
    }
}
