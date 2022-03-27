using SocialNet1.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Net.Domain.Base
{
    public class ImageComments : Entity
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
