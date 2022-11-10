using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Room_Service.Entities
{
    public class Workspace
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }
        [BsonRequired]
        public string name { get; set; } = null!;
        public string? imageFile { get; set; }

        [BsonElement("files")]
        public List<Files>? files { get; set; }

        [BsonIgnore]
        public List<Room>? rooms { get; set; }
    }
}