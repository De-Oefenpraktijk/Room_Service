using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Room_Service.Contracts;
using Room_Service.DTO;
using Room_Service.Entities;

namespace Room_Service.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkspaceController : Controller
    {
        private readonly IWorkspaceService _workspaceService;
        private readonly ILogger<RoomController> _log;

        public WorkspaceController(IWorkspaceService workspaceService, ILogger<RoomController> log)
        {
            _workspaceService = workspaceService;
            _log = log;
        }


        [HttpPost]
        [ProducesResponseType(typeof(WorkspaceDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Workspace>> CreateWorkspace([FromBody] WorkspaceDTO workspace)
        {
            try {
            var result = await _workspaceService.CreateWorkspace(workspace);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem with creating workspace");
                return BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Workspace>> GetUserRooms()
        {
            try
            {
                var result = await _workspaceService.GetWorkspaces();
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem with retrieving workspaces");
                return BadRequest();
            }
        }
    }
}

