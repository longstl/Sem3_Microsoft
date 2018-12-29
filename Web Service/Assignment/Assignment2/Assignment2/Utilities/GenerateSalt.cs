using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Utilities
{
    public class GenerateSalt
    {
        public static string saltStr(int lengthSalt)
        {
            const string chars = "abcdefghiklmnopqrstvxyzABCDEFGHIKLMNOPQRSTVXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, lengthSalt)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}