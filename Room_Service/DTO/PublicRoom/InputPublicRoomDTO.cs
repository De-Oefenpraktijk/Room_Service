using System.ComponentModel.DataAnnotations;
using Room_Service.Entities;

namespace Room_Service.DTO
{
    public class InputPublicRoomDTO
    {
        [Required]
        public string RoomName { get; set; }

        [Required]
        public string HostId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public string WorkspaceId { get; set; } 
    }
}
