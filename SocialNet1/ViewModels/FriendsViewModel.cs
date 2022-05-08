using SocialNet1.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.ViewModels
{
    public class FriendsViewModel
    {
        public UserDTO User { get; set; }

        public ICollection<UserDTO> Friends { get; set; }

        public ICollection<UserDTO> Subscribers { get; set; }

        public ICollection<UserDTO> Subscriptions { get; set; }

        public ICollection<UserDTO> All { get; set; }
    }
}
