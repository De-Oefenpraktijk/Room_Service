﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Room_Service.Entities;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Room_Service.DTO
{
    public class OutputWorkspaceDTO
    {
        public string? id { get; set; }
        [Required]
        public string name { get; set; }

        public Files? imageFile { get; set; }

        public List<FileDTO>? files { get; set; }

        public List<OutputRoomDTO>? rooms { get; set; }
    }
}

