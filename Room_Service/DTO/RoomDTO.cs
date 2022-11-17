using System.ComponentModel.DataAnnotations;
using Room_Service.Entities;

namespace Room_Service.DTO
{
    public class RoomDTO
    {
        [Required]
        public string hostUser { get; set; }

        [Required]
        public IEnumerable<string> invitedUsers { get; set; }

        [Required]
        public DateTime scheduledDate { get; set; }

        [Required]
        public string workspaceId { get; set; }
    }
}
