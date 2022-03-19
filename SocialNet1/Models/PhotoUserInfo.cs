﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Models
{
    public class PhotoUserInfo
    {
        public string AuthorImage { get; set; }

        public string AuthorFormat { get; set; }

        public string AuthorUserName { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorSecondName { get; set; }

        public string AuthorCoordinatesImage { get; set; }

        public string DateTime { get; set; }

        public string Comment { get; set; }

        public int LikeCount { get; set; }
    }
}
