using Social_Net.Domain.Group;
using SocialNet1.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.Group
{
    public class GroupImages : Images 
    {
        [Required]
        public virtual ICollection<GroupImageComments> Coments { get; set; } = new List<GroupImageComments>();

        [Required]
        public virtual ICollection<GroupLike> GroupLikes { get; set; } = new List<GroupLike>();

        [Required]
        public int RepostCount { get; set; } = 0;
    }
}
