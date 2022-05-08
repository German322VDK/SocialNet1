using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Models.API
{
    public class CommentModel
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int LikesCount { get; set; }

        public string Author { get; set; }

        public string Commenter { get; set; }

        public string FormatPhotoCom { get; set; }

        public string PhotoCom { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string DateTime { get; set; }

        public string Color { get; set; }

        public string Text { get; set; }

        public int HelpId { get; set; }

    }
}
