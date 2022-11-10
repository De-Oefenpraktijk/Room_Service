using System;
using System.ComponentModel.DataAnnotations;
using Room_Service.Entities;

namespace Room_Service.DTO
{
    public class FileDTO
    {
        [Required]
        public string FileUrl { get; set; } = null!;
        [Required]
        public string FileName { get; set; } = null!;
    }
}

