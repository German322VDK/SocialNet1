using SocialNet1.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Social_Net.Domain.Base
{
    public class ImageComments : Entity
    {
        [Required]
        public int HelpId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
