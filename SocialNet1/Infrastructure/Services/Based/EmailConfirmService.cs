using Social_Net.Domain.Security;
using SocialNet1.Database.SQlite.Context;
using SocialNet1.Infrastructure.Interfaces.Based;
using System.Collections.Generic;
using System.Linq;

namespace SocialNet1.Infrastructure.Services.Based
{
    public class EmailConfirmService : IEmailConfirm
    {
        private readonly SocialNetDBSQlite _db;

        public EmailConfirmService(SocialNetDBSQlite db)
        {
            _db = db;
        }
        public bool Set(string mail, string hash)
        {
            var item = _db.EmailConfirms.Where(el => el.Email == mail);

            if(item.ToList().Count == 0)
            {
                using (_db.Database.BeginTransaction())
                {
                    _db.EmailConfirms.Add(new EmailConfirm 
                    { 
                        Email = mail,
                        Hash = hash
                    });

                    _db.SaveChanges();

                    _db.Database.CommitTransaction();
                }

                return true;
            }
            else if(item.ToList().Count == 1)
            {
                using (_db.Database.BeginTransaction())
                {
                    _db.EmailConfirms.FirstOrDefault(el => el.Email == mail).Hash = hash;

                    _db.SaveChanges();

                    _db.Database.CommitTransaction();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public ICollection<EmailConfirm> GetAll() =>
            _db.EmailConfirms.ToList();

        public EmailConfirm Get(string mail) =>
          GetAll().SingleOrDefault(el => el.Email == mail);

    }
}
