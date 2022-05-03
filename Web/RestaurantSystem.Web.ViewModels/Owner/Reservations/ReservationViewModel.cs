namespace RestaurantSystem.Web.ViewModels.Owner.Reservations
{
    using System;

    using RestaurantSystem.Data.Models.Reservations;
    using RestaurantSystem.Services.Mapping;

    public class ReservationViewModel : IMapFrom<Reservation>
    {
        public string Id { get; init; }

        public DateTime CreatedOn { get; init; }

        public string FirstName { get; init; }

        public string PhoneNumber { get; init; }

        public DateTime Date { get; init; }

        public int PeopleCount { get; init; }

        public string ReservationStatus { get; init; }

        public string RestaurantId { get; init; }

        public string UserId { get; init; }
    }
}
