using System;
using System.ComponentModel.DataAnnotations;
using Room_Service.Entities;

namespace Room_Service.DTO
{
    public class FileDTO
    {
        public string? fileUrl { get; set; }
        
        public string? fileName { get; set; }

        public FileDTO ()
        {
            this.fileName = "";
            this.fileUrl = "";  
        }

        public FileDTO(string fileName, string fileUrl)
        {
            this.fileName = fileName;
            this.fileUrl = fileUrl;
        }
    }
}

