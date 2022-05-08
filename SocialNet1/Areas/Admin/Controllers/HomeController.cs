using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using SocialNet1.Domain.Base;
using SocialNet1.Infrastructure.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin,God")]
    public class HomeController : Controller
    {
        private ILogInfo _logInfo;

        public HomeController(ILogInfo logInfo)
        {
            _logInfo = logInfo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FullLogs() =>
            View(_logInfo.GetFullLogs());


        public IActionResult ErrorLogs() =>
            View(_logInfo.GetErrorLogs());


        public IActionResult GetFile(string fileName)
        {
            var itemAr = fileName.Split("\\");

            var shortName = itemAr[itemAr.Length - 1];

            string file_type = "application/txt";
            return PhysicalFile(fileName, file_type, shortName);
        }
    }
}
