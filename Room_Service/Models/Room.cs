using System.ComponentModel.DataAnnotations;

namespace Room_Service.Models
{
    public class Room
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid HostUser { get; set; }

        [Required]
        public Guid InvitedUser { get; set; }

        public string Name { get; set; } = String.Empty;
    }
}
