using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace webapplication.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string EventName { get; set; }
        public string EventAddress { get; set; }
        public string EventDescription { get; set; }
        public string EventStartDate { get; set; }
        public string EventCreationDAte { get; set; }
        public string UserId { get; set; }

    }
}
