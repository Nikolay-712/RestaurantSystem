namespace RestaurantSystem.Web.ViewModels.Owner.Orders
{
    using System.Collections.Generic;
    using System.Linq;

    public class AllOrdersViewModel : PagingViewModel
    {
        public string RestaurantId { get; set; }

        public IEnumerable<OrderViewModel> AllOrders { get; init; }

        public IEnumerable<OrderViewModel> PendingOrders { get; init; }

        public int AllOrdersCount { get; init; }

        public int PendingOrdersCount => this.PendingOrders.Count();
    }
}
