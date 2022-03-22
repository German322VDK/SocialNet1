using Microsoft.AspNetCore.Mvc;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.ViewModels.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Components
{
    public class UserInfoViewComponent : ViewComponent
    {
        private IUser _user;

        public UserInfoViewComponent(IUser user) =>
            _user = user;

        public IViewComponentResult Invoke()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var userName = HttpContext.User.Identity.Name;

                var user = _user.Get(userName);

                var arr = user.Images.SingleOrDefault(el => el.ImageNumber == user.SocNetItems.CurrentImage)?.Image;

                string image, format;

                if (arr is null)
                {
                    var newImage = Image.FromFile("wwwroot/photo/def/anon.jpg");
                    var memorystream = new MemoryStream();
                    newImage.Save(memorystream, ImageFormat.Jpeg);
                    arr = memorystream.ToArray();
                }

                image = ImageMethods.GetStringFromByteArr(arr);

                format = ImageMethods.GetFormat(arr);

                return View("UserInfo", new UserInfoComponentViewModel 
                { 
                    Format = format,
                    Photo = image,
                    Color = UserMethods.GetColor(user.SocNetItems.X, user.SocNetItems.Y)
                });
            }
            else
            {
                return View();
            }
        }
    }
}
