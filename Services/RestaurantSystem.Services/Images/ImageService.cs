namespace RestaurantSystem.Services.Images
{
    using System;
    using System.IO;
    using System.Linq;
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
            if (this.ExsitingCoverImage(restaurantId))
            {
                await this.DeleteFileAsync(restaurantId);
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                var splitFileName = file.FileName.Split(".", StringSplitOptions.RemoveEmptyEntries);

                var fileName = $"{splitFileName[0]}{restaurantId}.{splitFileName[1]}";
                var imageObject = await this.storageClient
                    .UploadObjectAsync(this.bucketName, fileName, null, memoryStream);

                var image = new Image()
                {
                    Name = fileName,
                    ImageUrl = imageObject.MediaLink,
                    RestaurantId = restaurantId,
                };

                await this.applicationDbContext.AddAsync(image);
                await this.applicationDbContext.SaveChangesAsync();

                memoryStream.Close();
                return image.ImageUrl;
            }
        }

        private async Task DeleteFileAsync(string restaurantId)
        {
            var image = this.applicationDbContext
                .Images
                .FirstOrDefault(x => x.RestaurantId == restaurantId);

            this.applicationDbContext.Images.Remove(image);
            await this.applicationDbContext.SaveChangesAsync();

            await this.storageClient.DeleteObjectAsync(this.bucketName, image.Name);
        }

        private bool ExsitingCoverImage(string restaurantId)
        {
            var restaurant = this.applicationDbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == restaurantId);

            return restaurant.CoverImageUrl is null ? false : true;
        }
    }
}
