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
    public class WorkspaceController : Controller
    {
        private readonly IWorkspaceService _workspaceService;
        private readonly ILogger<RoomController> _log;
        private readonly IMapper _mapper;

        public WorkspaceController(IWorkspaceService workspaceService, ILogger<RoomController> log, IMapper mapper)
        {
            _workspaceService = workspaceService;
            _log = log;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(typeof(WorkspaceDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Workspace>> CreateWorkspace([FromBody] WorkspaceDTO workspace)
        {
            try {
            var result = await _workspaceService.CreateWorkspace(workspace);
                if (result != null)
                {
                    var resultDTO = _mapper.Map<Workspace, WorkspaceDTO>(result);
                    return Ok(resultDTO);
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
                    var resultDTO = _mapper.Map<IEnumerable<Workspace>, IEnumerable<WorkspaceDTO>>(result);
                    return Ok(resultDTO);
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

