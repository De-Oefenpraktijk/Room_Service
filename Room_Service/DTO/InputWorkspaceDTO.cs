using System.ComponentModel.DataAnnotations;
using Room_Service.Entities;

namespace Room_Service.DTO
{
    public class InputWorkspaceDTO
    {
        [Required]
        public string name { get; set; }

        public string? imageName { get; set; }

        public string? imageUri { get; set; }
 
        public IFormFile? inputImageFile { get; set; }

        public FileDTO? imageFile { get; set; } = new FileDTO();

        public List<FileDTO>? files { get; set; }

        public List<OutputRoomDTO>? rooms { get; set; }
    }
}

