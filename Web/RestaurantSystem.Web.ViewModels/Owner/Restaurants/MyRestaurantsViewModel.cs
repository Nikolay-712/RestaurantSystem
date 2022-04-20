using RestaurantSystem.Data.Models.Restaurants;
using RestaurantSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.ViewModels.Owner.Restaurants
{
    public class MyRestaurantsViewModel : IMapFrom<Restaurant>
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal DeliveryPeice { get; init; }

        public DateTime OpenIn { get; init; }

        public DateTime CloseIn { get; init; }
    }
}
