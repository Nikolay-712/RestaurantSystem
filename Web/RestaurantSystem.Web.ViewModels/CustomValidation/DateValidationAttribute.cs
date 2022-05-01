using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.ViewModels.CustomValidation
{
    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var reservationDate = (DateTime)value;
            var compareDate = DateTime.Compare(reservationDate, DateTime.UtcNow);

            if (compareDate < 0)
            {
                return new ValidationResult("Не може да резервирате за отминала дата.");
            }

            var ts = reservationDate - DateTime.UtcNow;

            if (ts.Days > 14)
            {
                return new ValidationResult("Резервацията трябва да бъде в рамките на 14 дни.");
            }

            return ValidationResult.Success;
        }
    }
}
