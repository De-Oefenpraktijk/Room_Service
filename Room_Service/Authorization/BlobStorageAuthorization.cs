using System;
using Azure.Identity;
using Azure.Storage.Blobs;

namespace Room_Service.Authorization
{
	public class BlobStorageAuthorization
	{
		

            public BlobServiceClient GetBlobServiceClient(string accountName)
            {
                BlobServiceClient client = new(
                    new Uri($"https://{accountName}.blob.core.windows.net"),
                    new DefaultAzureCredential());

                return client;
            }
        
	}
}

