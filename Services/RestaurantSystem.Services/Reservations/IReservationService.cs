namespace RestaurantSystem.Services.Reservations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RestaurantSystem.Web.ViewModels.Owner.Reservations;
    using RestaurantSystem.Web.ViewModels.Reservations;

    public interface IReservationService
    {
        Task<bool> SendReservationAsync(ReservationInputViewModel reservationInput, string userId);

        IEnumerable<T> AllResarvations<T>();

        AllReservationsViewModel GetAllReservations(string restaurantId);

        Task<bool> ChangeReservationStatusAsync(string resarvationId, string status);

        Task<string> GetUserPhoneNumberAsync(string userId);

    }
}
