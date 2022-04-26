﻿using Social_Net.Domain.Group;
using SocialNet1.Domain.Group;
using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IGroup
    {
        ICollection<GroupDTO> GetAll();

        ICollection<GroupDTO> Get(string[] groups);

        GroupDTO Get(string group);

        bool AddPhoto(byte[] arr, string groupName);

        bool DeletePhoto(string groupName, int imageId);

        bool AddPhotoLike(string groupName, string userName, int imageId);

        bool DeletePhotoLike(string groupName, string userName, int imageId);

        GroupImageComments AddCommentToPhoto(string groupName, string userName, string text, int imageId);
    }
}
