using SocialNet1.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.ViewModels
{
    public class ProfileViewModel
    {
        public string CoordName { get; set; }

        public string CoordImage { get; set; }

        public UserDTO User { get; set; }
    }
}
