﻿namespace SocialNet1.Models.Static
{
    public static class APIUrls
    {
        #region User

        public const string ADD_IMAGE = "api/image/addcom";
        public const string DELETE_IMAGE = "api/image/delete";

        public const string DELETE_COM = "api/image/deletecom";
        
        public const string ADD_IMAGE_LIKE = "api/image/addlike";
        public const string DELETE_IMAGE_LIKE = "api/image/deletelike";

        public const string ADD_IMAGE_COM_LIKE = "api/image/addlikecom";
        public const string DELETE_IMAGE_COM_LIKE = "api/image/deletelikecom";

        public const string CONFIRM_EMAIL = "api/registr/confirm";
        public const string GIVE_HASH = "api/registr/givehash";

        public const string ADD_FRIEND = "api/friend/add";
        public const string DELETE_FRIEND = "api/friend/delete";

        public const string SET_STATUS = "api/user/setstatus";
        public const string DELETE_POST = "api/user/deletepost";
        public const string ADD_LIKE_POST = "api/user/addlikepost";
        public const string DELETE_LIKE_POST = "api/user/deletelikepost";
        public const string ADD_LIKE_COM_POST = "api/user/addlikecompost";
        public const string DELETE_LIKE_COM_POST = "api/user/deletelikecompost";
        public const string ADD_COM_POST = "api/user/addcompost";
        public const string DELETE_COM_POST = "api/user/deletecompost";

        public const string SET_AVA = "Profile/SetAva";
        public const string SET_COORD = "Profile/SetCoord";

        #endregion

        #region Group

        public const string DELETE_GROUP_IMAGE = "api/groupimage/delete";

        public const string ADD_GROUP_IMAGE_LIKE = "api/groupimage/addlike";
        public const string DELETE_GROUP_IMAGE_LIKE = "api/groupimage/deletelike";

        public const string ADD_GROUP_IMAGE_COM = "api/groupimage/addcom";

        #endregion

        public const string ADD_MESSAGE = "api/message/add";
        public const string DELETE_MESSAGE = "api/message/delete";
        public const string UPDATE_MESSAGE = "api/message/update";
    }
}
