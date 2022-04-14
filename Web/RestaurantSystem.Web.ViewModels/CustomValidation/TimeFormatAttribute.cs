namespace RestaurantSystem.Web.ViewModels.CustomValidation
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class TimeFormatAttribute : ValidationAttribute
    {
        private const string Pattern = "^(2[0-3]|[01]?[0-9]):([0-5]?[0-9])$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var regex = new Regex(Pattern);
            var inputTime = value as string;

            if (!regex.IsMatch(inputTime))
            {
                return new ValidationResult("Не валиден часови формат,(използвайте формат --:-- часа)");
            }

            return ValidationResult.Success;
        }
    }
}
