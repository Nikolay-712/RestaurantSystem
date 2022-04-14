namespace RestaurantSystem.Web.ViewModels.CustomValidation
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using RestaurantSystem.Data;

    internal class ExistingNameAttribute : ValidationAttribute
    {
        private readonly DesignTimeDbContextFactory factory = new DesignTimeDbContextFactory();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var applicationDbContext = this.factory.CreateDbContext(new string[] { });

            var existingName = applicationDbContext
                .Restaurants
                .Select(x => x.Name)
                .ToList()
                .Contains(value as string);

            if (existingName)
            {
                return new ValidationResult("Името вече се използва, изберете друго)");
            }

            return ValidationResult.Success;
        }
    }
}
