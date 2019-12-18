using System;
using System.Security.Cryptography;
using webapplication.Controllers;
using Xunit;

namespace webapp.Tests
{
    public class AuthControllerTest
    {
        [Fact]
        public static void HashPassword_HashedValueNotEmpty()
        {
            string password = "slaptas";

            string hashed = AuthController.HashPassword(password);

            Assert.True(hashed != null);
            Assert.IsType<string>(hashed);
        }

        [Fact]
        public static void HashPassword_HashedValueTypeIsString()
        {
            string password = "slaptas";

            string hashed = AuthController.HashPassword(password);

            Assert.IsType<string>(hashed);
        }

        [Fact]
        public static void HashPassword_HashesCorrectly()
        {
            string password = "slaptas";

            string hashed = AuthController.HashPassword(password);

            //-----
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var rfc = new Rfc2898DeriveBytes(password, salt, 1996);

            byte[] hash = rfc.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string Password = Convert.ToBase64String(hashBytes);
            //----
            byte[] _hashBytes = Convert.FromBase64String(Password);
            byte[] _salt = new byte[16];
            Array.Copy(_hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1996);
            byte[] _hash = pbkdf2.GetBytes(20);

            byte[] forTest = new byte[20];

            for (int i = 0; i < 20; i++)
            {
                forTest[i] = _hashBytes[i + 16];
            }
            //----
            Assert.Equal(_hash, forTest);
        }
    }
}
