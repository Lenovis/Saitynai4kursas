using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapplication.Services;
using webapplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        public readonly EventService _eventService;
        public EventController(EventService e)
        {
            _eventService = e;
        }

        // GET: api/values
        //Greiciausiai sitas bus nenaudojamas, kol kas testavimui tik paliktas
        [HttpGet]
        public ActionResult<List<Event>> Get() =>
            _eventService.Get();


        // GET api/values/5
        [HttpGet("{id}", Name = "GetEvent")]
        public ActionResult<Event> Get(string id)
        {
            var e = _eventService.Get(id);

            if(e == null)
            {
                return NotFound();
            }
            return e;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Event> Create(Event e)
        {
            _eventService.Create(e);

            return CreatedAtRoute("GetEvent", new { id = e.Id.ToString() }, e);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, Event eventIn)
        {
            var e = _eventService.Get(id);

            if(e == null)
            {
                return NotFound();
            }
            _eventService.Update(id, eventIn);
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var e = _eventService.Get(id);

            if(e == null)
            {
                return NotFound();
            }
            _eventService.Remove(e.Id);

            return NoContent();
        }
    }
}
