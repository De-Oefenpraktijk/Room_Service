using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Room_Service.Contracts;
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
        [ProducesResponseType(typeof(OutputWorkspaceDTO), (int)HttpStatusCode.OK)]
        [Authorize()]
        public async Task<ActionResult<OutputWorkspaceDTO>> GetUserRooms([FromRoute]string userid, [FromRoute]string workspaceid)
        {
            try {
            var result = await _roomService.GetUserRooms(userid, workspaceid);
                if (result != null) {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem with room retrieval by user and workspace");
                return BadRequest(ex.Message);
            }
        }

        [Route("room/{roomid}")]
        [HttpGet]
        [ProducesResponseType(typeof(OutputWorkspaceDTO), (int)HttpStatusCode.OK)]
        [Authorize()]
        public async Task<ActionResult<OutputWorkspaceDTO>> GetRoomByID(string roomid)
        {
            try
            {
                var result = await _roomService.GetRoomByID(roomid);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem with room retrieval by id");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(OutputRoomDTO), (int)HttpStatusCode.OK)]
        [Authorize()]
        public async Task<ActionResult<OutputRoomDTO>> CreateRoom([FromBody] InputRoomDTO room)
        {
            try {
            var result = await _roomService.CreateRoom(room);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (InvitedYourselfException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem creating room");
                return BadRequest(ex.Message);
            }
        }
    }
}
