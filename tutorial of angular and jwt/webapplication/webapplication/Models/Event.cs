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
        public string EventStartTime { get; set; }
        public string EventEndDate { get; set; }
        public string EventEndTime { get; set; }
        public string EventTravelTime { get; set; }
        public string EventRepeat { get; set; }
        public string EventPriority { get; set; }

        public string EventCreationDate { get; set; }

        public string UserId { get; set; }

    }
}
