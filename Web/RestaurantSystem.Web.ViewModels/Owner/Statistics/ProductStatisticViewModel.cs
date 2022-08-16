namespace RestaurantSystem.Web.ViewModels.Owner.Statistics
{
    using System.Collections.Generic;

    public class ProductStatisticViewModel
    {
        public string Name { get; init; }

        public int InOrders { get; init; }

        public int Ordered { get; init; }

        public IEnumerable<int> Rating { get; init; }

    }
}
