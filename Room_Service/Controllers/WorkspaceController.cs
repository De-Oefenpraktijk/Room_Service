using System.Net;
using Microsoft.AspNetCore.Mvc;
using Room_Service.Contracts;
using Room_Service.DTO;
using Azure.Storage.Blobs;
using System.Threading;
using Azure.Storage;
using Azure.Storage.Sas;
using SharpCompress.Common;
using Room_Service.Data;

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
        public async Task<ActionResult<OutputWorkspaceDTO>> CreateWorkspace([FromForm] InputWorkspaceDTO workspace)
        {

            //get blob details
            const string AccountName = "oefenpraktijkstorageacc";
            const string AccountKey = "wOzd9fNz/IKcurS7vkv49WJ9wYTe8Y7avYYcvfFrAt04GpSmO/Y8kb82UeLjau0El1y5txLUoj75+AStcMviFg==";
            const string ContainerName = "workspace-images";


            try
            {

                string blobStorageConnnectionString = "DefaultEndpointsProtocol=https;AccountName=oefenpraktijkstorageacc;AccountKey=wOzd9fNz/IKcurS7vkv49WJ9wYTe8Y7avYYcvfFrAt04GpSmO/Y8kb82UeLjau0El1y5txLUoj75+AStcMviFg==;EndpointSuffix=core.windows.net";
                string blobStorageContainerName = "workspace-images";
                BlobContainerClient container = new BlobContainerClient(blobStorageConnnectionString, blobStorageContainerName);

                BlobClient blob;
                FileStream stream;
                if (workspace.inputImageFile == null)
                {
                    workspace.imageName = "test";
                    blob = container.GetBlobClient(workspace.imageName);

                    return NotFound();
                }

                workspace.imageName = await SaveImage(workspace.inputImageFile);
                blob = container.GetBlobClient(workspace.imageName);
                stream = System.IO.File.OpenRead(workspace.imageName);

                //upload image to blob storage
                Azure.Response<Azure.Storage.Blobs.Models.BlobContentInfo> response =
                    await container.UploadBlobAsync(workspace.imageName, stream);



                Uri blobContainerUri = new(string.Format("https://{0}.blob.core.windows.net/{1}",
                    AccountName, ContainerName));

                StorageSharedKeyCredential storageSharedKeyCredential =
                    new(AccountName, AccountKey);

                BlobContainerClient blobContainerClient =
                    new(blobContainerUri, storageSharedKeyCredential);

                string relevantUri = blobContainerUri.ToString() + "/" + workspace.imageName;

                workspace.imageFile = new FileDTO(workspace.imageName, relevantUri);

                OutputWorkspaceDTO result = await _workspaceService.CreateWorkspace(workspace);

                return new ObjectResult(result);
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
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName));
            using (FileStream fileStream = new FileStream(imageName, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }


        [NonAction]
        public static async Task UploadStream
    (BlobContainerClient containerClient, string localFilePath)
        {
            string fileName = Path.GetFileName(localFilePath);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            FileStream fileStream = System.IO.File.OpenRead(localFilePath);
            await blobClient.UploadAsync(fileStream, true);
            fileStream.Close();
        }


    }
}

