using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;

namespace angelissaPastryShop.Services
{
    public class UploadService
    {
        private readonly string _connectionString;
        private readonly string _containerName = "imagenes";

        public UploadService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AzureBlobStorage");
        }

        public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            var blobClient = blobContainerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = "image/png" });

            return blobClient.Uri.ToString(); // Retorna la URL de la imagen
        }
    }
}
