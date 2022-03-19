using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Base
{
    public class Images : Entity
    {
        [Required]
        public int ImageNumber { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [Required]
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
