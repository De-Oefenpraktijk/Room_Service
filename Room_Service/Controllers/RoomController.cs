using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Room_Service.Contracts;
using Room_Service.Data;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly ILogger<RoomController> _log;

        public RoomController(IRoomService roomService, ILogger<RoomController> log)
        {
            _roomService = roomService;
            _log = log;
        }

        [Route("{workspaceid}/{userid}")]
        [HttpGet]
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WorkspaceDTO>> GetUserRooms([FromRoute]string userid, [FromRoute]string workspaceid)
        {
            try {
            var result = await _roomService.GetUserRooms(userid, workspaceid);
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem with room retrieval by user and workspace");
                return BadRequest();
            }
        }

        [Route("room/{roomid}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Workspace>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RoomDTO>> GetRoomByID(string roomid)
        {
            try
            {
                var result = await _roomService.GetRoomByID(roomid);
                if (result.rooms == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (InvitedYourselfException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem with room retrieval by id");
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Workspace>> CreateRoom([FromBody] Room room)
        {
            try {
            var result = await _roomService.CreateRoom(room);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem creating room");
                return BadRequest();
            }
        }
    }
}
