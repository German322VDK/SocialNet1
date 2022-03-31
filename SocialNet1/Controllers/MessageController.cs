using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialNet1.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
