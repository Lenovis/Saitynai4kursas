using System;
using System.Collections.Generic;
using System.Linq;
using webapplication.Models;

namespace webapplication.Services
{
    public class EventServiceFake : IEventService
    {
        public readonly List<Event> _events;

        public EventServiceFake()
        {
            _events = new List<Event>()
            {
                new Event()
                {
                    Id="5df9cb2ebefdf24095aa3256",
                    EventName="Pirmas eventas",
                    EventAddress="",EventDescription="",
                    EventStartDate="2019-12-18",
                    EventStartTime="",
                    EventEndDate="2019-12-18",
                    EventEndTime="",
                    EventTravelTime="",
                    EventRepeat="",
                    EventPriority="notImportant",
                    EventCreationDate="12/18/2019 11:53:36",
                    UserId="5dee34ce743cd90e14ab5f81"
                },
                new Event()
                {
                    Id="5df9cb2ebefdf24095aa3257",
                    EventName="Antras eventas",
                    EventAddress="",EventDescription="",
                    EventStartDate="2019-12-18",
                    EventStartTime="",
                    EventEndDate="2019-12-18",
                    EventEndTime="",
                    EventTravelTime="",
                    EventRepeat="",
                    EventPriority="notImportant",
                    EventCreationDate="12/18/2019 11:53:36",
                    UserId="5dee34ce743cd90e14ab5f81"
                },
                new Event()
                {
                    Id="5df9cb2ebefdf24095aa3258",
                    EventName="Trecias eventas",
                    EventAddress="",EventDescription="",
                    EventStartDate="2019-12-18",
                    EventStartTime="",
                    EventEndDate="2019-12-18",
                    EventEndTime="",
                    EventTravelTime="",
                    EventRepeat="",
                    EventPriority="notImportant",
                    EventCreationDate="12/18/2019 11:53:36",
                    UserId="5dee34ce743cd90e14ab5f81"
                }
            };

        }

        public Event Create(Event e)
        {
            _events.Add(e);
            return e;
        }

        public List<Event> Get(string userId, bool b) =>
            _events.FindAll(e => e.UserId == userId).ToList();


        public Event Get(string id) =>
            _events.Find(e => e.Id == id);

        public void Remove(Event eventIn)
        {
            _events.Remove(eventIn);
        }

        public void Remove(string id)
        {
            var found = _events.FirstOrDefault(x => x.Id == id);
            _events.Remove(found);
        }

        public void Update(string id, Event eventIn)
        {
            var found = _events.FirstOrDefault(x => x.Id == id);
            Remove(found);
            _events.Add(eventIn);
        }
    }
}
