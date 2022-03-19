using SocialNet1.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Identity
{
    public class UserImages : Images 
    {
        [Required]
        public virtual ICollection<UserImageComments> Coments { get; set; } = new List<UserImageComments>();

        [Required]
        public int LikeCount { get; set; } = 0;

        [Required]
        public int RepostCount { get; set; } = 0;
    }
}
