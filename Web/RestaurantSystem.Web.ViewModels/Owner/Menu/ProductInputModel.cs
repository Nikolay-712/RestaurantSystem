namespace RestaurantSystem.Web.ViewModels.Owner.Menu
{
    using System.ComponentModel.DataAnnotations;

    using RestaurantSystem.Data.Models.Products;

    using static RestaurantSystem.Common.GlobalConstants;

    public class ProductInputModel
    {
        [Display(Name = "Името")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 30, ErrorMessage = LenghtErrorMessage, MinimumLength = 5)]
        public string Name { get; init; }

        [Required(ErrorMessage = RequiredFieldMessage)]
        [EnumDataType(typeof(Category), ErrorMessage = CategoryErrorMesage)]
        public Category Category { get; init; }

        [Display(Name = "Описанието")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [StringLength(maximumLength: 100, ErrorMessage = LenghtErrorMessage, MinimumLength = 10)]
        public string Description { get; init; }

        [Display(Name = "Цената")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [Range(minimum: 0.1, maximum: 100, ErrorMessage = PriceErrorMessage)]
        public decimal Price { get; init; }

        [Display(Name ="Грамажа")]
        [Required(ErrorMessage = RequiredFieldMessage)]
        [Range(minimum: 0.1, maximum: 500, ErrorMessage = WeightErrorMessage)]
        public int Weight { get; init; }

        public string RestaurantId { get; init; }
    }
}
