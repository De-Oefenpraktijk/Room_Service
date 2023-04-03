using System.ComponentModel.DataAnnotations;
using Room_Service.Entities;

namespace Room_Service.DTO
{
    public class OutputRoomDTO
    {
        public string? roomId { get; set; }

        [Required]
        public string roomName { get; set; }

        [Required]
        public string hostId { get; set; }

        [Required]
        public IEnumerable<string> invitedIds { get; set; }

        [Required]
        public DateTime scheduledDate { get; set; }

        [Required]
        public string workspaceId { get; set; }
    }
}
