using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

       public ImageAPIController(IUser user)
        {
            _user = user;
        }

        //[HttpPost]
        //public void Add([FromBody] AddCommentImageModel data)
        //{

        //}

        [HttpGet("add")]
        public PhotoUserInfo Add(string text, string sender, string recipient, int imageId)
        {
            var com = _user.AddCommentToPhoto(recipient, sender, text, imageId);

            if (com == null)
                return null;

            var senderUser = _user.Get(sender);

            var curImageArr = senderUser.Images.SingleOrDefault(el => el.ImageNumber == senderUser.SocNetItems.CurrentImage).Image;

            var format = ImageMethods.GetFormat(curImageArr);

            var curImage = ImageMethods.GetStringFromByteArr(curImageArr);

            return new PhotoUserInfo
            {
                DateTime = com.DateTime.ToString("D"),
                Comment = com.Text,
                LikeCount = com.LikeCount,
                AuthorFirstName = senderUser.FirstName,
                AuthorSecondName = senderUser.SecondName,
                AuthorUserName = senderUser.UserName,
                AuthorCoordinatesImage = $"photo/coordinates/{senderUser.SocNetItems.X}d{senderUser.SocNetItems.Y}.jpg",
                AuthorImage = curImage,
                AuthorFormat = format
            };
        }
    }
}
