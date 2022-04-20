namespace RestaurantSystem.Web.ViewModels.CustomValidation
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using RestaurantSystem.Data;
    using RestaurantSystem.Web.ViewModels.Owner.Restaurants;

    internal class ExistingNameAttribute : ValidationAttribute
    {
        private readonly DesignTimeDbContextFactory factory = new DesignTimeDbContextFactory();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var applicationDbContext = this.factory.CreateDbContext(new string[] { });
            var inputType = validationContext.ObjectType.Name;

            var restaurantName = value.ToString().ToLower();

            var existingName = applicationDbContext
                    .Restaurants
                    .Select(x => x.Name.ToLower())
                    .ToList()
                    .Contains(restaurantName);

            if (inputType == "RegisterRestaurantInputModel")
            {
                if (existingName)
                {
                    return new ValidationResult("Името вече се използва, изберете друго)");
                }
            }

            if (inputType == "EditRestaurantInputModel")
            {
                var editModel = validationContext.ObjectInstance as EditRestaurantInputModel;
                var restaurant = applicationDbContext.Restaurants.FirstOrDefault(x => x.Name.ToLower() == restaurantName);

                if (existingName && restaurant.Id != editModel.Id)
                {
                    return new ValidationResult("Името вече се използва, изберете друго)");
                }
            }

            return ValidationResult.Success;
        }
    }
}
