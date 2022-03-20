using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Social_Net.Domain.Security;
using SocialNet1.Domain.Group;
using SocialNet1.Domain.Identity;
using SocialNet1.Domain.Message;
using SocialNet1.Domain.PostCom;

namespace SocialNet1.Database.SQlite.Context
{
    public class SocialNetDBSQlite : IdentityDbContext<UserDTO, RoleDTO, string>
    {
        public DbSet<GroupDTO> Groups { get; set; }

        public DbSet<UserGroupStatus> UserGroupStatuses { get; set; }

        public DbSet<PostDTO> PostDTOs { get; set; }

        public DbSet<ChatDTO> Chats { get; set; }

        public DbSet<EmailConfirm> EmailConfirms { get; set; }

        public SocialNetDBSQlite(DbContextOptions<SocialNetDBSQlite> options) : base(options) { }
    }
}
