using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PizzaShop
{
    public class Cryptography
    {
        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }

        public static bool Compare(string clearTextPassword, string hash)
        {
            return hash.Equals(Hash(clearTextPassword));
        }
    }
}