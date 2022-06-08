namespace RestaurantSystem.Services.Images
{
    using System.IO;
    using System.Threading.Tasks;

    using Google.Apis.Auth.OAuth2;
    using Google.Cloud.Storage.V1;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Images;

    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;

        public ImageService(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
            this.storageClient = StorageClient.Create(this.googleCredential);
            this.bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
        }

        public async Task<string> UploadFileAsync(string restaurantId, IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var fileName = file.FileName + restaurantId;
                var imageObject = await this.storageClient.UploadObjectAsync(this.bucketName, fileName, null, memoryStream);

                var image = new Image()
                {
                    Name = file.FileName,
                    ImageUrl = imageObject.MediaLink,
                    RestaurantId = restaurantId,
                };

                await this.applicationDbContext.AddAsync(image);
                await this.applicationDbContext.SaveChangesAsync();

                memoryStream.Close();
                return image.ImageUrl;
            }
        }
    }
}
