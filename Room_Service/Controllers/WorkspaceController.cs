using System.Net;
using Microsoft.AspNetCore.Mvc;
using Room_Service.Contracts;
using Room_Service.DTO;
using Azure.Storage.Blobs;
using System.Threading;
using Azure.Storage;
using Azure.Storage.Sas;

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

            //get blob details
            const string AccountName = "oefenpraktijkstorageacc";
            const string AccountKey = "wOzd9fNz/IKcurS7vkv49WJ9wYTe8Y7avYYcvfFrAt04GpSmO/Y8kb82UeLjau0El1y5txLUoj75+AStcMviFg==";
            const string ContainerName = "workspace-images";


            try {

                string blobStorageConnnectionString = "DefaultEndpointsProtocol=https;AccountName=oefenpraktijkstorageacc;AccountKey=wOzd9fNz/IKcurS7vkv49WJ9wYTe8Y7avYYcvfFrAt04GpSmO/Y8kb82UeLjau0El1y5txLUoj75+AStcMviFg==;EndpointSuffix=core.windows.net";
                string blobStorageContainerName = "workspace-images";
                BlobContainerClient container = new BlobContainerClient(blobStorageConnnectionString, blobStorageContainerName);

                //make (or get) temporary
                // BlobClient blob = container.GetBlobClient("temp");
                // Generate unique image name
                BlobClient blob;
                FileStream stream;
                if (workspace.inputImageFile == null)
                {
                    //set default
                    workspace.imageName = "test";
                    blob = container.GetBlobClient(workspace.imageName);


                    // skip upload

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

                Uri relevantUri = GetServiceSasUriForContainer(blobContainerClient);

                workspace.imageUri = relevantUri.ToString();
                workspace.imageFile = new FileDTO(workspace.imageName, workspace.imageUri);

                //workspace.imageName = await SaveImage(workspace.imageFile);
                OutputWorkspaceDTO result = await _workspaceService.CreateWorkspace(workspace);

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

        private static Uri GetServiceSasUriForContainer(BlobContainerClient containerClient,
                                          string storedPolicyName = null)
        {
            // Check whether this BlobContainerClient object has been authorized with Shared Key.
            if (containerClient.CanGenerateSasUri)
            {
                // Create a SAS token that's valid for one hour.
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = containerClient.Name,
                    Resource = "c"
                };

                if (storedPolicyName == null)
                {
                    sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
                    sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);
                }
                else
                {
                    sasBuilder.Identifier = storedPolicyName;
                }

                Uri sasUri = containerClient.GenerateSasUri(sasBuilder);
                Console.WriteLine("SAS URI for blob container is: {0}", sasUri);
                Console.WriteLine();

                return sasUri;
            }
            else
            {
                Console.WriteLine(@"BlobContainerClient must be authorized with Shared Key 
                          credentials to create a service SAS.");
                return null;
            }
        }
    }
}

