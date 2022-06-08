namespace RestaurantSystem.Services.Images
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        Task<string> UploadFileAsync(string restaurantId, IFormFile file);
    }
}
