namespace RestaurantSystem.Services.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Reservations;
    using RestaurantSystem.Services.Mapping;
    using RestaurantSystem.Services.Notifications;
    using RestaurantSystem.Services.Users;
    using RestaurantSystem.Web.ViewModels.Owner.Reservations;
    using RestaurantSystem.Web.ViewModels.Reservations;

    using static RestaurantSystem.Common.GlobalConstants;

    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IUserService userService;
        private readonly INotificationService notificationService;

        public ReservationService(
            ApplicationDbContext applicationDbContext,
            IUserService userService,
            INotificationService notificationService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userService = userService;
            this.notificationService = notificationService;
        }

        public async Task<bool> SendReservationAsync(ReservationInputViewModel reservationInput, string userId)
        {
            var date = reservationInput.Date.ToShortDateString() + " " + reservationInput.Time;
            var reservationDate = DateTime.Parse(date);

            var reservation = new Reservation
            {
                CreatedOn = DateTime.UtcNow,
                FirstName = reservationInput.FirstName,
                PhoneNumber = reservationInput.PhoneNumber,
                Date = reservationDate,
                PeopleCount = reservationInput.PeopleCount,
                ReservationStatus = ReservationStatus.Pending,
                RestaurantId = reservationInput.RestaurantId,
                UserId = userId,
            };

            await this.applicationDbContext.Reservations.AddAsync(reservation);
            var result = await this.applicationDbContext.SaveChangesAsync();

            if (result == 1 && reservationInput.SavePhone)
            {
                await this.userService.SavePhoneNumberAsync(userId, reservationInput.PhoneNumber);
            }

            if (result == 1)
            {
                await this.notificationService.SendNotificationAsync(
                 userId, string.Format(Message.ReservationNotificationMessage, Message.ReservationPending), "Reservation", reservation.Id);
            }

            return result == 1 ? true : false;
        }

        public IEnumerable<T> AllResarvations<T>()
        {
            return this.applicationDbContext
                 .Reservations.To<T>();
        }

        public AllReservationsViewModel GetAllReservations(string restaurantId)
        {
            var reservations = this.applicationDbContext.Reservations
                .Where(x => x.RestaurantId == restaurantId)
                .To<ReservationViewModel>().ToList();

            var allReservations = new AllReservationsViewModel
            {
                Rservations = reservations
                .OrderByDescending(x => x.ReservationStatus)
                .ThenBy(x => x.Date),
            };

            return allReservations;
        }

        public async Task<bool> ChangeReservationStatusAsync(string resarvationId, string status)
        {
            var resarvation = this.applicationDbContext
                .Reservations
                .FirstOrDefault(x => x.Id == resarvationId);

            ReservationStatus parseResult;
            var validStatus = Enum.TryParse<ReservationStatus>(status, out parseResult);

            if (resarvation == null || !validStatus)
            {
                return false;
            }

            resarvation.ReservationStatus = parseResult;

            this.applicationDbContext.Update(resarvation);
            var result = await this.applicationDbContext.SaveChangesAsync();

            if (result == 1)
            {
                var message = string.Empty;

                if (parseResult == ReservationStatus.Canceled)
                {
                    message = string
                        .Format(Message.ReservationNotificationMessage, Message.ReservationCanceled);
                }

                if (parseResult == ReservationStatus.Approved)
                {
                    message = string
                        .Format(Message.ReservationApproved, resarvation.Date.ToString("dd.MM.yy HH:mm"));
                }

                await this.notificationService.ChanageNotificationMessageAsync(
                    resarvation.UserId, message, "Reservation", resarvation.Id);
            }

            return result == 1 ? true : false;
        }

        public async Task<string> GetUserPhoneNumberAsync(string userId)
        {
            var user = await this.userService.GetUserByIdAsync(userId);
            var phoneNumber = user.PhoneNumber;

            return phoneNumber;
        }
    }
}
