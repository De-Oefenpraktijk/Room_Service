using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Room_Service.Entities
{
    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId roomId { get; set; }

        public string hostId { get; set; }

        public IEnumerable<string> invitedIds { get; set; }

        public DateTime scheduledDate { get; set; }

        public ObjectId workspaceId { get; set; }
    }
}
