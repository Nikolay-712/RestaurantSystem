namespace RestaurantSystem.Web.ViewModels.Owner.Reservations
{
    using System.Collections.Generic;
    using System.Linq;

    public class AllReservationsViewModel : PagingViewModel
    {
        public IEnumerable<ReservationViewModel> Rservations { get; set; }

        public int PendingReservationsCount
            => this.Rservations.Where(x => x.ReservationStatus == "Pending").Count();
    }
}
