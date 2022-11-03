using System;
using System.ComponentModel.DataAnnotations;
using Room_Service.Entities;

namespace Room_Service.DTO
{
    public class FileDTO
    {
        [Required]
        public string fileUrl { get; set; } = null!;
        [Required]
        public string fileName { get; set; } = null!;

        public FileDTO(Files file)
        {
            fileUrl = file.fileUrl;
            fileName = file.fileName;
        }
    }
}

