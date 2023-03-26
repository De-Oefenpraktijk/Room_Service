using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
        private readonly IWebHostEnvironment _hostEnvironment;

        private readonly ILogger<RoomController> _log;

        public WorkspaceController(IWorkspaceService workspaceService, ILogger<RoomController> log)
        {
            _workspaceService = workspaceService;
            _log = log;
        }


        [HttpPost]
        [ProducesResponseType(typeof(OutputWorkspaceDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OutputWorkspaceDTO>> CreateWorkspace([FromForm] InputWorkspaceDTO workspace)
        {
            try {

                workspace.imageName = await SaveImage(workspace.imageFile);
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


        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}

