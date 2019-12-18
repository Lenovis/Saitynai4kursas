using System;
using webapplication.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace webapplication.Services
{
    public class EventService : IEventService
    {
        private readonly IMongoCollection<Event> _events;

        public EventService(IProjectDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _events = database.GetCollection<Event>(settings.EventsCollectionName);
        }
        //dar nezinau ar sitas butina
        public List<Event> Get(string userId, bool b) =>
            _events.Find(e => e.UserId == userId).ToList();

        public Event Get(string id) =>
            _events.Find(e => e.Id == id).FirstOrDefault();

        public Event Create(Event e)
        {
            _events.InsertOne(e);
            return e;
        }

        public void Update(string id, Event eventIn) =>
            _events.ReplaceOne(e => e.Id == id, eventIn);

        public void Remove(Event eventIn) =>
            _events.DeleteOne(e => e.Id == eventIn.Id);

        public void Remove(string id) =>
            _events.DeleteOne(e => e.Id == id);
    }
}
