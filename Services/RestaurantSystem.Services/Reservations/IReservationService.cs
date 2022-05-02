using RestaurantSystem.Data.Models.Reservations;
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

        IEnumerable<T> AllResarvations<T>();

        Task<bool> ChangeReservationStatusAsync(string resarvationId, string status);

        Task<string> GetUserPhoneNumberAsync(string userId);

    }
}
