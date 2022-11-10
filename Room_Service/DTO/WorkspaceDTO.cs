using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Room_Service.Entities;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Room_Service.DTO
{
    public class WorkspaceDTO
    {
        public ObjectId Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? ImageFile { get; set; }

        public List<FileDTO>? Files { get; set; }
    }
}

