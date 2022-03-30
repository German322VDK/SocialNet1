using Social_Net.Domain.Base;
using Social_Net.Domain.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Identity
{
    public class UserImageComments : ImageComments 
    {

        [Required]
        public virtual ICollection<UserCommentLike> UserCommentLikes { get; set; } = new List<UserCommentLike>();
    }
}
