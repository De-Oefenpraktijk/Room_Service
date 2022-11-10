using System.ComponentModel.DataAnnotations;
using Room_Service.Entities;

namespace Room_Service.DTO
{
    public class RoomDTO
    {
        [Required]
        public string HostUser { get; set; }

        [Required]
        public IEnumerable<string> InvitedUsers { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public string WorkspaceId { get; set; }
    }
}
