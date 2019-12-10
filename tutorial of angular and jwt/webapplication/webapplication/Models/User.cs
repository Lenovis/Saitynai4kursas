using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace webapplication.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserLogIn { get; set; }
        public string UserPassword { get; set; }
    }
}
