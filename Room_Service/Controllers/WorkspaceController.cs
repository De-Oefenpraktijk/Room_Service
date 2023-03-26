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
        [ProducesResponseType(typeof(OutputWorkspaceDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OutputWorkspaceDTO>> CreateWorkspace([FromBody] InputWorkspaceDTO workspace)
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
        [ProducesResponseType(typeof(OutputWorkspaceDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OutputWorkspaceDTO>> GetAllWorkspaces()
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

