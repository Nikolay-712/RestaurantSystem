namespace RestaurantSystem.Web.ViewModels.CustomValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    public class ImageValidationAttribute : ValidationAttribute
    {
        private int maxFileSize = 2;
        private string[] fileExtensions = new string[] { "jpg", "png", "jpeg" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var maxSize = this.maxFileSize * 1024 * 1024;
                var contentTyep = file.ContentType.Split("/", StringSplitOptions.RemoveEmptyEntries);

                if (file.Length > maxSize)
                {
                    return new ValidationResult($"Позволения размер на изображението е до {this.maxFileSize} MB.");
                }

                if (!this.fileExtensions.Contains(contentTyep[1]))
                {
                    return new ValidationResult
                        ($"Формата не се подържа, използвайте изображение в формат " +
                        $"{string.Join(" ", this.fileExtensions)}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
