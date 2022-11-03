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
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [Route("{workspaceid}/{userid}")]
        [HttpGet]
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WorkspaceDTO>> GetUserRooms([FromRoute]string userid, [FromRoute]string workspaceid)
        {
            var result = await _roomService.GetUserRooms(userid, workspaceid);
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("room/{roomid}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Workspace>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RoomDTO>> GetRoomByID(string roomid)
        {
            var result = await _roomService.GetRoomByID(roomid);
            if (result.rooms == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RoomDTO>> UpdateRoom([FromBody] Room product)
        {
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Workspace>> DeleteRoom([FromBody] Room product)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Workspace>> CreateRoom([FromBody] Room room)
        {
            var result = await _roomService.CreateRoom(room);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
