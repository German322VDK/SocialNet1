using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNet1.Infrastructure.Methods
{
    public static class StringGenerationMethods
    {
        public static string Generate(int length, bool engLow = true, bool EngUp = false,
            bool numbers = false, bool rusLow = false, bool rusUp = false)
        {
            var random = new Random();

            var result = new StringBuilder(length);

            string whiteList = "";

            if (engLow)
                whiteList = string.Concat(whiteList, FromTo('a', 'z').ToStr());

            if (EngUp)
                whiteList = string.Concat(whiteList, FromTo('A', 'Z').ToStr());

            if (numbers)
                whiteList = string.Concat(whiteList, FromTo('0', '9').ToStr());

            if (rusLow)
                whiteList = string.Concat(whiteList, FromTo('а', 'я').ToStr());

            if (rusUp)
                whiteList = string.Concat(whiteList, FromTo('А', 'Я').ToStr());

            whiteList.ToArray();

            for (int i = 0; i < length; i++)
            {
                var index = random.Next(0, whiteList.Length);

                result.Append(whiteList[index]);
            }

            return result.ToString();
        }

        private static IEnumerable<char> FromTo(char start, char end)
        {
            if (start >= end)
                throw new ArgumentOutOfRangeException();

            for (int i = start; i < end; i++)
            {
                yield return (char)i;
            }
        }

        private static string ToStr(this IEnumerable<char> chars) =>
            new string(chars.ToArray());

    }
}
