using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Models.Static
{
    public static class APIUrls
    {
        public const string ADD_IMAGE = "api/image/addcom";
        public const string DELETE_IMAGE = "api/image/delete";

        public const string DELETE_COM = "api/image/deletecom";

        public const string ADD_IMAGE_LIKE = "api/image/addlike";
        public const string DELETE_IMAGE_LIKE = "api/image/deletelike";

        public const string CONFIRM_EMAIL = "api/registr/confirm";
        public const string GIVE_HASH = "api/registr/givehash";

        public const string ADD_FRIEND = "api/friend/add";
        public const string DELETE_FRIEND = "api/friend/delete";

        public const string SET_STATUS = "api/user/setstatus";

        public const string SET_AVA = "Profile/SetAva";

    }
}
