using System.Net;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, ILogger<RoomController> log, IMapper mapper)
        {
            _roomService = roomService;
            _log = log;
            _mapper = mapper;
        }

        [Route("{workspaceid}/{userid}")]
        [HttpGet]
        [ProducesResponseType(typeof(WorkspaceDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WorkspaceDTO>> GetUserRooms([FromRoute]string userid, [FromRoute]string workspaceid)
        {
            try {
            var result = await _roomService.GetUserRooms(userid, workspaceid);
                if (result != null) {
                    var resultDTO = _mapper.Map<Workspace, WorkspaceDTO>(result);
                    return Ok(resultDTO);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem with room retrieval by user and workspace");
                return BadRequest();
            }
        }

        [Route("room/{roomid}")]
        [HttpGet]
        [ProducesResponseType(typeof(WorkspaceDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WorkspaceDTO>> GetRoomByID(string roomid)
        {
            try
            {
                var result = await _roomService.GetRoomByID(roomid);
                if (result != null)
                {
                    var resultDTO = _mapper.Map<Workspace, WorkspaceDTO>(result);
                    return Ok(resultDTO);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem with room retrieval by id");
                return BadRequest();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(RoomDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RoomDTO>> CreateRoom([FromBody] RoomDTO room)
        {
            try {
            var result = await _roomService.CreateRoom(room);
                if (result != null)
                {
                    var resultDTO = _mapper.Map<Room, RoomDTO>(result);
                    return Ok(resultDTO);
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
                return BadRequest();
            }
        }
    }
}
