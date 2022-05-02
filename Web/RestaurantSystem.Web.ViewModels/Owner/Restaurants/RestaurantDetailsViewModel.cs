using AutoMapper;
using RestaurantSystem.Data.Models.Restaurants;
using RestaurantSystem.Services.Mapping;
using RestaurantSystem.Web.ViewModels.Owner.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.ViewModels.Owner.Restaurants
{
    public class RestaurantDetailsViewModel : IMapFrom<Restaurant>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ReservationViewModel> Reservations { get; set; }

        public int PendingReservationsCount
            => this.Reservations.Where(x => x.ReservationStatus == "Pending").Count();
    }
}
