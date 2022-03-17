using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNet1.Domain.Identity;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Methods
{
    public static class UserMethods
    {
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>0-gray, 1-blue, 2-yel, 3-green, 4-red</returns>
        public static string GetColor(int x, int y)
        {
            if (x == 0 && y == 0)
                return "gray";
            else if (x >= 0 && y >= 0)
                return "blue";
            else if (x > 0 && y <= 0)
                return "yellow";
            else if (x < 0 && y < 0)
                return "green";
            else 
                return "red";
        }
    }
}
