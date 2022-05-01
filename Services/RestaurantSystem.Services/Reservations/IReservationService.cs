using RestaurantSystem.Web.ViewModels.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Reservations
{
    public interface IReservationService
    {
        Task<int> SendReservationAsync(ReservationInputViewModel reservationInput, string userId);

        Task<string> GetUserPhoneNumberAsync(string userId);
    }
}
