using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Controllers
{
    public class ClashController : Controller
    {
        private readonly IUser _user;
        private readonly IClash _clash;
        private readonly ILogger<ClashController> _logger;
        public ClashController(IUser user, IClash clash, ILogger<ClashController> logger)
        {
            _logger = logger;
            _user = user;
            _clash = clash;
        }

        public IActionResult Index()
        {
            var userName = User.Identity.Name;

            var user = _user.Get(userName);

            if (user is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            var allClashs = _clash.GetAll().Where(cl => cl.Side1.IsReady || cl.Side2.IsReady).ToList();

            var userClashs = _clash.GetByUser(userName).Where(cl => cl.Side1.IsReady || cl.Side2.IsReady).ToList(); 

            return View(new ClashesViewModel 
            { 
                AllClashes = allClashs, 
                UserClashes = userClashs 
            });
        }
    }
}
