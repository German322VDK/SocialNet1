﻿using Microsoft.AspNetCore.Mvc;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.ViewModels.Components;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

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

                if(user is null)
                    return View();

                var arr = user.Images.SingleOrDefault(el => el.ImageNumber == user.SocNetItems.CurrentImage)?.Image;

                string image, format;

                if (arr is null)
                {
                    arr = NewImageMethods.GetByteArrFromFile("wwwroot/photo/def/anon.jpg");
                }

                image = NewImageMethods.GetStringFromByteArr(arr);

                format = NewImageMethods.GetFormat(arr);

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
