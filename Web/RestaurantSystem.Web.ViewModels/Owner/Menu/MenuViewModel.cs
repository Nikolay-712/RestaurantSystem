namespace RestaurantSystem.Web.ViewModels.Owner.Menu
{
    using System.Collections.Generic;

    public class MenuViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; init; }

        public string RestaurantId { get; set; }

        public string Category { get; set; }

    }
}
