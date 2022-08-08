namespace RestaurantSystem.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class AllUserOrdersViewModel : PagingViewModel
    {
        public IEnumerable<UserOrdersViewModel> AllOrders { get; set; }

        public IEnumerable<UserOrdersViewModel> OrdersInProgres { get; set; }

        public IEnumerable<UserOrdersViewModel> SentOrders { get; set; }
    }
}
