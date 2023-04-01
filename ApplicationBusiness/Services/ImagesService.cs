using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ApplicationBusiness.Services;
public class ImagesService
{
    private string StorageStringConnection { get; set; }
    private string ContainerName { get; set; }
    BlobContainerClient ContainerClient { get; set; }

    public ImagesService(IConfiguration configuration)
    {
        StorageStringConnection = configuration.GetConnectionString("StorageStringConnection") ?? "";
        ContainerName = configuration["BlobContainerName"] ?? "";

        BlobServiceClient client = new BlobServiceClient(StorageStringConnection);
        ContainerClient = client.GetBlobContainerClient(ContainerName);

        ContainerClient.CreateIfNotExists();
    }

    public string UploadImage(IFormFile image)
    {
        string fileName = GenerateFileName();

        BlobClient blobClient = ContainerClient.GetBlobClient(fileName);

        using (Stream file = image.OpenReadStream())
        {
            blobClient.Upload(file, GetBlobHttpHeaders(image));
        }

        return blobClient.Uri.ToString();
    }

    #region private

    private string GenerateFileName()
    {
        return $"{GenerateUniqueId()}.jpg";
    }

    private string GenerateUniqueId()
    {
        return Guid.NewGuid().ToString();
    }

    private BlobHttpHeaders GetBlobHttpHeaders(IFormFile image) 
    {
        return new BlobHttpHeaders() { ContentType = image.ContentType };
    }

    #endregion
}
