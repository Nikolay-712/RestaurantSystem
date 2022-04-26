namespace RestaurantSystem.Web.ViewModels.Owner.Menu
{
    using System.Collections.Generic;

    public class MenuViewModel : PagingViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; init; }

        public string RestaurantId { get; init; }

        public string Category { get; init; }
    }
}
