using SocialNet1.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace SocialNet1.Domain.PostCom
{
    public class Like : Entity
    {

        [Required]
        public string Likers { get; set; }

        [Required]
        public Emoji Emoji { get; set; }
    }
}
