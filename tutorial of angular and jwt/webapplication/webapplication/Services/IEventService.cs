using System;
using System.Collections.Generic;
using webapplication.Models;

namespace webapplication.Services
{
    public interface IEventService
    {
        public List<Event> Get(string userId, bool b);

        public Event Get(string id);

        public Event Create(Event e);

        public void Update(string id, Event eventIn);

        public void Remove(Event eventIn);

        public void Remove(string id);
    }
}
