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
    public class PublicRoomController : ControllerBase
    {
        private readonly IPublicRoomService _roomService;
        private readonly ILogger<RoomController> _log;

        public PublicRoomController(IPublicRoomService roomService, ILogger<RoomController> log)
        {
            _roomService = roomService;
            _log = log;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OutputPublicRoomDTO), (int)HttpStatusCode.OK)]
        [Authorize("create:public-rooms")]
        public async Task<ActionResult<OutputPublicRoomDTO>> Create([FromBody] InputPublicRoomDTO room)
        {
            try
            {
                var result = await _roomService.CreateRoom(room);
                if (result != null)
                {
                    return Ok(result);
                }
                return Conflict();
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem creating a room");
                return BadRequest(ex.Message);
            }
        }

        [Route("{workspaceid}")]
        [HttpGet]
        [ProducesResponseType(typeof(OutputPublicRoomDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OutputPublicRoomDTO>> GetPublicRoomsOfWorkspace([FromRoute]string workspaceid)
        {
            try
            {
                var result = await _roomService.GetPublicRoomsOfWorkspace(workspaceid);
                if (result != null)
                {
                    return Ok(result);
                }
                return Conflict();
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem getting public rooms");
                return BadRequest(ex.Message);
            }
        }

    }
}
