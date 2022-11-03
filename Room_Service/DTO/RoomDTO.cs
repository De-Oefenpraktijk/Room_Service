using System.ComponentModel.DataAnnotations;
using Room_Service.Entities;

namespace Room_Service.DTO
{
    public class RoomDTO
    {
        [Required]
        public string RoomId { get; set; }
        [Required]
        public string HostUser { get; set; }

        public IEnumerable<string> InvitedUsers { get; set; }

        public DateTime ScheduledDate { get; set; }

        public RoomDTO(Room room)
        {
            RoomId = room.roomId!;
            HostUser = room.hostId;
            InvitedUsers = room.invitedIds;
            ScheduledDate = room.scheduledDate;
        }
    }
}
