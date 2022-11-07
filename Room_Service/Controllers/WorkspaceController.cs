using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Room_Service.Contracts;
using Room_Service.DTO;
using Room_Service.Entities;
using Room_Service.Services.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Room_Service.Controllers
{
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
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WorkspaceDTO>> CreateWorkspace([FromBody] Workspace workspace)
        {
            try {
            var result = await _workspaceService.CreateWorkspace(workspace);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem with creating workspace");
                return BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WorkspaceDTO>> GetUserRooms()
        {
            try
            {
                var result = await _workspaceService.GetWorkspaces();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex, "Problem with retrieving workspaces");
                return BadRequest();
            }
        }
    }
}

