namespace RestaurantSystem.Data.Models.Reservations
{
    using System;

    public class Reservation
    {
        public Reservation()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FirstName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Date { get; set; }

        public int PeopleCount { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        public string RestaurantId { get; set; }

        public string UserId { get; set; }
    }
}
