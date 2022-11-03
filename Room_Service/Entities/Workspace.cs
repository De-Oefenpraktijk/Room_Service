using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Room_Service.Entities
{
    public class Workspace
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRequired]
        public string Name { get; set; } = null!;
        public string? ImageFile { get; set; }

        [BsonElement("items")]
        [JsonPropertyName("items")]
        public List<Files>? files { get; set; }
    }
}