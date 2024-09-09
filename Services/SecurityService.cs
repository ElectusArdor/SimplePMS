using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Simple_PMS.Services
{
    internal class SecurityService
    {
        internal static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
    }
}
