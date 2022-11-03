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
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string? ImageFile { get; set; }

        [Required]
        public List<RoomDTO>? rooms { get; set; }

        public List<FileDTO>? files { get; set; }

        public WorkspaceDTO(Workspace workspace)
        {
            Id = workspace.Id!;
            Name = workspace.Name;
            ImageFile = workspace.ImageFile;
            files = workspace.files != null ? workspace.files.Select(x => new FileDTO(x)).ToList() : files = null;
        }
    }
}

