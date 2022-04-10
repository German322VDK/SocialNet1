using SocialNet1.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Social_Net.Domain.Security
{
    public class EmailConfirm : Entity
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Hash { get; set; }
    }
}
