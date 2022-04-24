using Microsoft.AspNetCore.Http;
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

        public GroupImageAPIController(ILogger<GroupImageAPIController> logger, IGroup group)
        {
            _logger = logger;
            _group = group;
        }

        [HttpGet("addlike")]
        public bool AddImageLike(string groupname, string username, int imageid)
        {
            return true;
        }

        [HttpGet("deletelike")]
        public bool DeleteImageLike(string groupname, string username, int imageid)
        {
            return true;
        }
    }
}
