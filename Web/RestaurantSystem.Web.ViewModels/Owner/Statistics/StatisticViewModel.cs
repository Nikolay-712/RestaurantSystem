namespace RestaurantSystem.Web.ViewModels.Owner.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RestaurantSystem.Data.Models.Restaurants;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Web.ViewModels.Menu;

    public class StatisticViewModel : IMapFrom<Restaurant>
    {
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<OrderStatisticViewModel> Orders { get; set; }

        public int OrdersCount => this.Orders.Count();

        public decimal OrdersRevenu
            => this.Orders.Select(x => x.TotaalSum).Sum();

    }
}
