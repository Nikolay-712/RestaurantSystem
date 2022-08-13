using AutoMapper;
using RestaurantSystem.Data.Models.Orders;
using RestaurantSystem.Data.Models.Reservations;
using RestaurantSystem.Data.Models.Restaurants;
using RestaurantSystem.Services.Mapping;
using RestaurantSystem.Web.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.ViewModels.Reservations
{
    public class UserReservationViewModel : IMapFrom<Reservation>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Date { get; set; }

        public int PeopleCount { get; set; }

        public string ReservationStatus { get; set; }

        public string UserId { get; set; }
    }
}
