﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Data;
using SocialNet1.Domain.Identity;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.ViewModels;

namespace SocialNet1.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IUser _user;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IUser user, ILogger<ProfileController> logger)
        {
            _user = user;
            _logger = logger;
        }

        public IActionResult Index(string userName = null)
        {
            UserDTO userDTO;

            var currantUserName = User.Identity!.Name;

            if (userName is not null)
                userDTO = _user.Get(userName);
            else
                userDTO = _user.Get(currantUserName);

            _logger.LogInformation($"{currantUserName} заходит на страницу {userDTO.UserName}");

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
            var currantUserName = User.Identity!.Name;

            _logger.LogInformation($"{currantUserName} пытается добавить фотографию");

            if (uploadedFile is null)
            {
                _logger.LogInformation($"Фото не пришло(");

                return RedirectToAction("Error", "Home");
            }
                

            var arr = ImageMethods.GetByteArrFromFile(uploadedFile);

            var result = ImageMethods.IsValid(arr);

            if (!result)
            {
                _logger.LogInformation($"Фото плохое(");

                return RedirectToAction("Error", "Home");
            }
               
            var resultAdding = _user.AddPhoto(arr, HttpContext.User.Identity.Name);

            if (!resultAdding)
            {
                _logger.LogInformation($"Фото почему то не добавилось(");

                return RedirectToAction("Error", "Home");
            }

            _logger.LogInformation($"{currantUserName} успешно добавил фотографию");

            return RedirectToAction("Index", "Profile");
        }
    }
}
