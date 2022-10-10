using Microsoft.EntityFrameworkCore;
using Room_Service.Models;
using Room_Service.DTO;

namespace Room_Service.Entities
{
    public class DBContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Room_Service.DTO.RoomDTO> RoomDTO { get; set; }
    }
}
