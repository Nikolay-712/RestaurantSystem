namespace RestaurantSystem.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.Linq;

    public class OrderViewModel
    {
        public IEnumerable<OrderProductViewModel> Products { get; set; }

        public decimal TotaalSum => this.Products.Select(x => x.Sum).Sum();
    }
}
