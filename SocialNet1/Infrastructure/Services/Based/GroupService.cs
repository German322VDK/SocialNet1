using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Group;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Services.Based
{
    public class GroupService : IGroup
    {
        private readonly SocialNetDBSQlite _db;

        public GroupService(SocialNetDBSQlite db)
        {
            _db = db;
        }

        #region BaseCRUD

        public ICollection<GroupDTO> GetAll() =>
            _db.Groups.ToList();

        public ICollection<GroupDTO> Get(string[] groups) =>
             GetAll().Where(el => groups.Contains(el.ShortGroupName) ).ToList();

        public GroupDTO Get(string group) =>
             GetAll().FirstOrDefault(el => el.ShortGroupName == group);



        #endregion


        #region Photo

        public bool AddPhoto(byte[] arr, string groupName)
        {
            var group = Get(groupName);

            if (group is null || arr is null)
                return false;

            int lastNum;
            if (group.Images.Count == 0)
                lastNum = 0;
            else
                lastNum = group.Images.Max(el => el.ImageNumber);

            var newLast = lastNum + 1;

            using (_db.Database.BeginTransaction())
            {
                _db.Groups
                    .FirstOrDefault(gr => gr.ShortGroupName == groupName)
                    .Images.Add(new GroupImages
                    {
                        Image = arr,
                        ImageNumber = newLast,
                    });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        #endregion
    }
}
