using System;
using System.Security.Cryptography;
using webapplication.Controllers;
using webapplication.Models;
using Xunit;

namespace webapp.Tests
{
    public class UserControllerTest
    {
        [Fact]
        public void UserController_ShouldReturnFalse()
        {
            bool expected = false;

            User user = new User { FirstName = "Martynas" };

            bool actual = UserController.isUserNull(user);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserController_ShouldReturnTrue()
        {
            bool expected = true;

            User user = new User
            {
                Id = "",
                FirstName = "",
                LastName = "",
                UserPassword = "",
                UserLogIn = ""
            };

            bool actual = UserController.isUserNull(user);

            Assert.Equal(expected, actual);
        }
    }
}
