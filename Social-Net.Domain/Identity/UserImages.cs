using SocialNet1.Domain.Base;
using SocialNet1.Domain.PostCom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Identity
{
    public class UserImages : Images 
    {
        [Required]
        public virtual ICollection<UserImageComments> Coments { get; set; } = new List<UserImageComments>();

        [Required]
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

        [Required]
        public int RepostCount { get; set; } = 0;
    }
}
