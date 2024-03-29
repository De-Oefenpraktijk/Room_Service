﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Room_Service.Entities
{
    public class PrivateRoom
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? roomId { get; set; } = null!;

        public string roomName { get; set; }

        public string hostId { get; set; }

        public IEnumerable<string> invitedIds { get; set; }

        public DateTime scheduledDate { get; set; }

        public string workspaceId { get; set; }
    }
}
