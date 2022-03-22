using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNet1.Data;
using SocialNet1.Domain.Identity;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IUser _user;
        public ProfileController(IUser user)
        {
            _user = user;
        }

        public IActionResult Index(string userName = null)
        {
            UserDTO userDTO;

            var currantUserName = User.Identity!.Name;

            if (userName is not null)
                userDTO = _user.Get(userName);
            else
                userDTO = _user.Get(currantUserName);

            var x = userDTO.SocNetItems.X;
            var y = userDTO.SocNetItems.Y;

            var coordName = CooridnatesNames.GetName(x, y);

            return View(new ProfileViewModel 
            { 
                User = userDTO,
                CoordName = coordName,
                CoordImage = $"photo/coordinates/{x}d{y}.jpg"
            });
        }

        public IActionResult AddImage(IFormFile uploadedFile)
        {
            if(uploadedFile is null)
                return RedirectToAction("Error", "Home");

            var arr = ImageMethods.GetByteArrFromFile(uploadedFile);

            var result = ImageMethods.IsValid(arr);

            if(!result)
                return RedirectToAction("Error", "Home");

            var resultAdding = _user.AddPhoto(arr, HttpContext.User.Identity.Name);

            if (!resultAdding)
                return RedirectToAction("Error", "Home");

            return RedirectToAction("Index", "Profile");
        }
    }
}
