using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Room_Service.Contracts;
using Room_Service.DTO;
using Room_Service.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Room_Service.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkspaceController : Controller
    {
        private readonly IWorkspaceService _workspaceService;

        public WorkspaceController(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        //[Route("rooms/{userid}")]
        //[HttpGet]
        //[ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<WorkspaceDTO>> GetUserRooms(string userid)
        //{
        //    var result = await _roomService.GetUserRooms(userid);
        //    if (result.rooms == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(result);
        //}

        //[Route("room/{roomid}")]
        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<Workspace>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<RoomDTO>> GetRoomByID(string roomid)
        //{
        //    var result = await _roomService.GetRoomByID(roomid);
        //    if (result.rooms == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(result);
        //}

        //[HttpPut]
        //[ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<RoomDTO>> UpdateRoom([FromBody] Room product)
        //{
        //    return Ok();
        //}

        //[HttpDelete]
        //[ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<Workspace>> DeleteRoom([FromBody] Room product)
        //{
        //    return Ok();
        //}

        [HttpPost]
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WorkspaceDTO>> CreateWorkspace([FromBody] Workspace workspace)
        {
            var result = await _workspaceService.CreateWorkspace(workspace);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}

