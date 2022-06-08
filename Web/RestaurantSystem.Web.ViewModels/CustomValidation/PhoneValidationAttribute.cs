namespace RestaurantSystem.Web.ViewModels.CustomValidation
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class PhoneValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;
            var patern = "^[0-9]{9}$";

            Regex regex = new Regex(patern);

            if (!regex.IsMatch(phoneNumber))
            {
                return new ValidationResult("Не валиден телефонен номер.");
            }


            return ValidationResult.Success;
        }
    }
}
