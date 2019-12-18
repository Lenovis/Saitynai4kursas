using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webapplication.Models;
using webapplication.Services;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace webapplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private IConfiguration _configuration;

        public static string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var rfc = new Rfc2898DeriveBytes(password, salt, 1996);

            byte[] hash = rfc.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string Password = Convert.ToBase64String(hashBytes);
            return Password;
        }

        public AuthController(UserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        private JwtSecurityToken theToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserLogIn),
                        new Claim(ClaimTypes.Role, "Admin")
                    };
            var tokeOptions = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(180),
                    signingCredentials: signinCredentials
                );
            return tokeOptions;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var allusers = _userService.Get();

            foreach (var _user in allusers)
            {
                if (_user.UserLogIn == user.UserLogIn)
                {
                    string userPassword = _user.UserPassword;
                    byte[] hashBytes = Convert.FromBase64String(userPassword);
                    byte[] salt = new byte[16];
                    Array.Copy(hashBytes, 0, salt, 0, 16);
                    var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 1996);
                    byte[] hash = pbkdf2.GetBytes(20);

                    for(int i = 0; i < 20; i++)
                    {
                        if (hashBytes[i + 16] != hash[i])
                            throw new UnauthorizedAccessException();
                    }
                    User userForToken = new User
                    {
                        UserLogIn = user.UserLogIn
                    };

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(theToken(userForToken));
                    return Ok(new { Token = tokenString });

                }                
            }
            return Unauthorized();
        }

        [HttpPost, Route("register")]
        public IActionResult Register([FromBody]RegisterModel user)
        {

            if(user == null)
            {
                return BadRequest("Invalid client request");
            }

            var allUsers = _userService.Get();
            int countOfSameUsers = 0;
            
            foreach(var _user in allUsers)
            {
                if (user.UserLogIn == _user.UserLogIn)
                    countOfSameUsers++;                                
            }

            
            if (countOfSameUsers == 0)
            {
                //uzregistruoja nauja vartotoja
                var _user = new User
                {
                    UserLogIn = user.UserLogIn,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserPassword = HashPassword(user.Password)
                };
                _userService.Create(_user);
                //ji priregistruoja
                User userForToken = new User
                {
                    UserLogIn = user.UserLogIn
                };
                var tokenString = new JwtSecurityTokenHandler().WriteToken(theToken(userForToken));
                return Ok(new { Token = tokenString });

            }
            else
                return BadRequest("Client is already registered");
        }

    }
}