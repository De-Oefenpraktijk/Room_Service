namespace Room_Service.Entities
{
    public class Room
    {
        public Guid roomId { get; set; }
        public string hostId { get; set; } = null!;
        public string invitedId { get; set; } = null!;
        public DateTime scheduledDate { get; set; }
    }
}
