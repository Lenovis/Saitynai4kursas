using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapplication.Models;
using webapplication.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapplication.Controllers
{
    [ApiController]
    [Route("api/[controller]"),Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public static bool isUserNull(User user)
        {
            bool temp = false;
            User empty = new User
            {
                Id = "",
                FirstName = "",
                LastName = "",
                UserPassword = "",
                UserLogIn = ""
            };
            if (user.Id == empty.Id)
            {
                temp = true;
            }
            else if(user == null)
            {
                temp = true;
            }
            return temp;
        }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get() =>
            _userService.Get();

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if(isUserNull(user))
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _userService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User userIn)
        {
            var user = _userService.Get(id);

            if(user == null)
            {
                return NotFound();
            }

            _userService.Update(id, userIn);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if(user == null)
            {
                return NotFound();
            }

            _userService.Remove(user.Id);

            return Ok();
        }
    }
}
