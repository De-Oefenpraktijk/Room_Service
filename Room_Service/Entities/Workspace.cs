﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Room_Service.Entities
{
    public class Workspace
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }

        public string name { get; set; }

        public Files? imageFile { get; set; }

        [BsonElement("files")]
        public List<Files>? files { get; set; }

        [BsonIgnore]
        public List<PrivateRoom>? rooms { get; set; }
    }
}