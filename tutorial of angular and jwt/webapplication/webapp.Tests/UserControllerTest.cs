using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using webapplication.Controllers;
using webapplication.Models;
using webapplication.Services;
using Xunit;

namespace webapp.Tests
{
    public class UserControllerTest
    {
        UserController _userController;
        IUserService _userService;

        public UserControllerTest()
        {
            _userService = new UserServiceFake();
            _userController = new UserController(_userService);
        }

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

        [Theory]
        [InlineData("5dee34ce743cd90e14ab5f81")]
        [InlineData("5dee34f8743cd90e14ab5f82")]
        public void Get_ShouldReturnUser(string id)
        {
            var user = _userService.Get(id);

            Assert.IsType<User>(user);
        }

        [Theory]
        [InlineData("5dee34ce743cd90e14ab5f81", "Martynas", "Petruska", "marpet", "slaptas")]
        [InlineData("5dee34f8743cd90e14ab5f82", "Brigita", "Macaite", "brimac", "slaptas")]
        public void Get_ShouldReturnAllDataCorrectly(string id, string firstName,
            string lastName, string logIn, string password)
        {
            var user = _userService.Get(id);

            Assert.Equal(id, user.Id);
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);
            Assert.Equal(logIn, user.UserLogIn);
            Assert.Equal(password, user.UserPassword);
        }

        [Theory]
        [InlineData("5dee34ce743cd90e14ab5f81", "Martynas", "Petruska", "marpet", "slaptas")]
        [InlineData("5dee34f8743cd90e14ab5f82", "Brigita", "Macaite", "brimac", "slaptas")]
        public void Get_UserController_ShouldReturnUser(string id, string firstName,
            string lastName, string logIn, string password)
        {
            var user = _userController.Get(id);

            Assert.Equal(id, user.Value.Id);
            Assert.Equal(firstName, user.Value.FirstName);
            Assert.Equal(lastName, user.Value.LastName);
            Assert.Equal(logIn, user.Value.UserLogIn);
            Assert.Equal(password, user.Value.UserPassword);
        }
        /// <summary>
        /// Get all users
        /// </summary>
        [Fact]
        public void Get_ShouldGetAllUsers()
        {
            var user = _userService.Get();

            Assert.Equal(2, user.Count);
        }
        /// <summary>
        /// get all users from UserController
        /// </summary>
        [Fact]
        public void Get_UserController_AllUsers()
        {
            var user = _userController.Get();

            Assert.Equal(2, user.Value.Count);
        }
        /// <summary>
        /// Updates user with UserService
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="logIn"></param>
        /// <param name="password"></param>
        [Theory]
        [InlineData("5dee34ce743cd90e14ab5f81", "Martynas", "Petruska", "marpet1", "slaptass")]
        [InlineData("5dee34f8743cd90e14ab5f82", "Brigita", "Macaite", "brimac1", "slaptass")]
        public void Update_ShouldUpdateUser(string id, string firstName,
            string lastName, string logIn, string password)
        {
            User user = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserLogIn = logIn,
                UserPassword = password
            };

            _userService.Update(id, user);

            Assert.Equal(user, _userService.Get(id));
        }
        /// <summary>
        /// Deletes users from UserController
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="logIn"></param>
        /// <param name="password"></param>
        [Theory]
        [InlineData("5dee34ce743cd90e14ab5f81", "Martynas", "Petruska", "marpet1", "slaptass")]
        [InlineData("5dee34f8743cd90e14ab5f82", "Brigita", "Macaite", "brimac1", "slaptass")]
        public void Update_ShouldReturnOk(string id, string firstName,
            string lastName, string logIn, string password)
        {
            User user = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserLogIn = logIn,
                UserPassword = password
            };

            var ok = _userController.Update(id, user);

            Assert.IsType<OkResult>(ok);
        }
        /// <summary>
        /// Deletes users with UserController
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="logIn"></param>
        /// <param name="password"></param>
        [Theory]
        [InlineData("5dee34ce743cd90e14ab5f81", "Martynas", "Petruska", "marpet1", "slaptass")]
        [InlineData("5dee34f8743cd90e14ab5f82", "Brigita", "Macaite", "brimac1", "slaptass")]
        public void Delete_UserControler_ShouldReturnOk(string id, string firstName,
            string lastName, string logIn, string password)
        {
            User user = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserLogIn = logIn,
                UserPassword = password
            };

            var ok = _userController.Delete(id);

            Assert.IsType<OkResult>(ok);
        }
        /// <summary>
        /// Passed right users
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="logIn"></param>
        /// <param name="password"></param>
        [Theory]
        [InlineData("5dee34ce743cd90e14ab5f81", "Martynas", "Petruska", "marpet1", "slaptass")]
        [InlineData("5dee34f8743cd90e14ab5f82", "Brigita", "Macaite", "brimac1", "slaptass")]
        public void Delete_ShouldDeleteUser(string id, string firstName,
            string lastName, string logIn, string password)
        {
            User user = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserLogIn = logIn,
                UserPassword = password
            };

            _userService.Remove(user);

            var users = _userService.Get();

            Assert.Equal(1, users.Count);
        }
        /// <summary>
        /// Passed wrong users
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="logIn"></param>
        /// <param name="password"></param>
        [Theory]
        [InlineData("5dee34ce743cd90e14ab5", "Martynas", "Petruska", "marpet1", "slaptass")]
        [InlineData("5dee34f8743cd90e14ab5f892", "Brigita", "Macaite", "brimac1", "slaptass")]
        public void Delete_ShouldNOTDeleteUser(string id, string firstName,
            string lastName, string logIn, string password)
        {
            User user = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserLogIn = logIn,
                UserPassword = password
            };

            _userService.Remove(user);

            var users = _userService.Get();

            Assert.Equal(2, users.Count);
        }

        [Theory]
        [InlineData("5dee34ce743cd90e14ab5", "Martynas", "Petruska", "marpet1", "slaptass")]
        [InlineData("5dee34f8743cd90e14ab5f892", "Brigita", "Macaite", "brimac1", "slaptass")]
        public void Create_ShouldReturnActionResult(string id, string firstName,
            string lastName, string logIn, string password)
        {
            User user = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserLogIn = logIn,
                UserPassword = password
            };

            var result = _userController.Create(user);
            var users = _userController.Get();

            //It returns Action result but not with full tail of it
            //It is Pass
            Assert.IsType<ActionResult>(result);
            Assert.Equal(3, users.Value.Count);
        }

        [Theory]
        [InlineData("5dee34ce743cd90e14ab5", "Martynas", "Petruska", "marpet1", "slaptass")]
        [InlineData("5dee34f8743cd90e14ab5f892", "Brigita", "Macaite", "brimac1", "slaptass")]
        public void Create_ShouldAddUserToList(string id, string firstName,
            string lastName, string logIn, string password)
        {
            User user = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserLogIn = logIn,
                UserPassword = password
            };

             _userController.Create(user);
            var users = _userController.Get();

            Assert.Equal(3, users.Value.Count);
        }
    }
}
