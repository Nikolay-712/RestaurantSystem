using RestaurantSystem.Data.Models.Reservations;
using RestaurantSystem.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.ViewModels.Owner.Reservations
{
    public class ReservationViewModel : IMapFrom<Reservation>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FirstName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Date { get; set; }

        public int PeopleCount { get; set; }

        public string ReservationStatus { get; set; }

        public string RestaurantId { get; set; }

        public string UserId { get; set; }
    }
}
