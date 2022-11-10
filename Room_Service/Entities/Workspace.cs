using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Room_Service.Entities
{
    public class Workspace
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }
        [BsonRequired]
        public string name { get; set; } = null!;
        public string? imageFile { get; set; }

        [BsonElement("items")]
        public List<Files>? files { get; set; }

        [BsonIgnoreIfDefault]
        [BsonElement("items")]
        public List<Room>? rooms { get; set; }
    }
}