using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Room_Service.Entities;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Room_Service.DTO
{
    public class InputWorkspaceDTO
    {
        [Required]
        public string name { get; set; }

        public string? FileName { get; set; }

        public IFormFile imageFile { get; set; }

        public string imageName { get; set; }


        public List<FileDTO>? files { get; set; }

        public List<OutputRoomDTO>? rooms { get; set; }
    }
}

