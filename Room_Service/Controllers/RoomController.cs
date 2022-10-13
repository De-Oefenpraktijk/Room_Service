using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Room_Service.DTO;
using Room_Service.Entities;
using Room_Service.Models;

namespace Room_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly DBContext _context;

        public RoomController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Room/GetRooms
        [HttpGet("GetRooms")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> Get()
        {
            var List = await _context.Rooms.Select(
                s => new RoomDTO
                {
                    Id = s.Id,
                    HostUser = s.HostUser,
                    InvitedUser = s.InvitedUser,
                    Name = s.Name,
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        // GET: api/Room/GetRoom/5
        [HttpGet("GetRoom/{id}")]
        public async Task<ActionResult<RoomDTO>> Get(Guid id)
        {
            RoomDTO? Room = await _context.Rooms.Select(
                    s => new RoomDTO
                    {
                        Id = s.Id,
                        HostUser = s.HostUser,
                        InvitedUser = s.InvitedUser,
                        Name = s.Name,
                    })
                .FirstOrDefaultAsync(s => s.Id == id);

            if (Room == null)
            {
                return NotFound();
            }
            else
            {
                return Room;
            }
        }

        // GET api/Room/GetMyRooms/5
        [HttpGet("GetMyRooms/{id}")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetMyRooms(Guid id)
        {
            var List = await _context.Rooms.Select(
                s => new RoomDTO
                {
                    Id = s.Id,
                    HostUser = s.HostUser,
                    InvitedUser = s.InvitedUser,
                    Name = s.Name,
                }
            ).Where(s => s.HostUser == id || s.InvitedUser == id).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        // POST: api/Room/CreateRoom
        [HttpPost("CreateRoom")]
        public async Task<HttpStatusCode> Post(RoomDTO Room)
        {
            var entity = new Room()
            {
                Id = Room.Id,
                HostUser = Room.HostUser,
                InvitedUser = Room.InvitedUser,
                Name = Room.Name,
            };

            _context.Rooms.Add(entity);
            await _context.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        // DELETE: api/Room/DeleteRoom/5
        [HttpDelete("DeleteRoom/{id}")]
        public async Task<HttpStatusCode> Delete(Guid id)
        {
            var entity = new Room()
            {
                Id = id
            };
            _context.Rooms.Attach(entity);
            _context.Rooms.Remove(entity);
            await _context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
