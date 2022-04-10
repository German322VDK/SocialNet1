using Social_Net.Domain.Security;
using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IEmailConfirm
    {
        public EmailConfirm Get(string mail);

        public ICollection<EmailConfirm> GetAll();

        public bool Set(string mail, string hash);


    }
}
