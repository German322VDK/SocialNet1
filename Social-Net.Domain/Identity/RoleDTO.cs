using Microsoft.AspNetCore.Identity;
using SocialNet1.Domain.Base;

namespace SocialNet1.Domain.Identity
{
    /// <summary>
    /// Роль
    /// </summary>
    public class RoleDTO : IdentityRole
    {
        public UserStatus Status { get; set; }

        public string Description { get; set; }
    }
}
