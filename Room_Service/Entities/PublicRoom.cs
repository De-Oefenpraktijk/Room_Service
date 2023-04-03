using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Room_Service.Entities
{
    public class PublicRoom
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? RoomId { get; set; } = null!;

        public string? RoomName { get; set; }

        public string? HostId { get; set; }

        public string? Description { get; set; }

        public DateTime? ScheduledDate { get; set; }

        public string? WorkspaceId { get; set; }
    }
}
