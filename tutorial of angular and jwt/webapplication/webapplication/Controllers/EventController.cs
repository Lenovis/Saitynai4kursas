using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapplication.Services;
using webapplication.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapplication.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize]
    public class EventController : ControllerBase
    {
        public readonly IEventService _eventService;
        public readonly IUserService _userService;
        public EventController(IEventService e, IUserService user)
        {
            _eventService = e;
            _userService = user;
        }

        // GET: api/values
        //Greiciausiai sitas bus nenaudojamas, kol kas testavimui tik paliktas
        [HttpGet]
        public string Get()
        {
            var userId = _userService.Get()
                .Where(x => x.UserLogIn == HttpContext.User.Claims.FirstOrDefault().Value)
                .Select(x => x.Id).FirstOrDefault();

            var e = _eventService.Get(userId, true);
            var a = JsonConvert.SerializeObject(e);
            return a;
        }


        // GET api/values/5
        [HttpGet("{id}", Name = "GetEvent")]
        public ActionResult<Event> Get(string id)
        {
            var e = _eventService.Get(id);

            if (e == null)
            {
                return NotFound();
            }
            return e;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Event> Create(Event e)
        {

            var userId = _userService.Get()
                .Where(x => x.UserLogIn == HttpContext.User.Claims.FirstOrDefault().Value)
                .Select(x => x.Id).FirstOrDefault();

            e.UserId = userId;
            e.EventCreationDate = DateTime.Now.ToString();

            _eventService.Create(e);
            return CreatedAtRoute("GetEvent", new { id = e.Id.ToString() }, e);
        }

        // PUT api/values/5
        [HttpPatch("{id}")]
        public IActionResult Update(string id, Event eventIn)
        {
            var e = _eventService.Get(id);
           
            if (e == null)
            {
                return NotFound();
            }
            eventIn.Id = e.Id;
            eventIn.UserId = e.UserId;
            eventIn.EventCreationDate = DateTime.Now.ToString();

            _eventService.Update(id, eventIn);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            char[] charsTotrim = { '"', '{', '\\', '}', ':'};
            id = id.Trim(charsTotrim);

            var e = _eventService.Get(id);

            if(e == null)
            {
                return NotFound();
            }
            _eventService.Remove(e.Id);

            return Ok();
        }
    }
}
