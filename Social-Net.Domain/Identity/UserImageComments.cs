using SocialNet1.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet1.Domain.Identity
{
    public class UserImageComments : Entity
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public int LikeCount { get; set; } = 0;

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
