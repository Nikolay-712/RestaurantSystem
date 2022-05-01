namespace RestaurantSystem.Services.Reservations
{
    using System;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Reservations;
    using RestaurantSystem.Services.Users;
    using RestaurantSystem.Web.ViewModels.Reservations;

    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IUserService userService;

        public ReservationService(ApplicationDbContext applicationDbContext, IUserService userService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userService = userService;
        }

        public async Task<int> SendReservationAsync(ReservationInputViewModel reservationInput, string userId)
        {
            var date = reservationInput.Date.ToString("dd.MM.yy") + " " + reservationInput.Time;
            var reservationDate = DateTime.Parse(date);

            var reservation = new Reservation
            {
                CreatedOn = DateTime.UtcNow,
                FirstName = reservationInput.FirstName,
                PhoneNumber = reservationInput.PhoneNumber,
                Date = reservationDate,
                PeopleCount = reservationInput.PeopleCount,
                IsConfirmed = false,
                RestaurantId = reservationInput.RestaurantId,
                UserId = userId,
            };

            await this.applicationDbContext.Reservations.AddAsync(reservation);
            var result = await this.applicationDbContext.SaveChangesAsync();

            if (result == 1 && reservationInput.SavePhone)
            {
                await this.userService.SavePhoneNumberAsync(userId, reservationInput.PhoneNumber);
            }

            return result;
        }

        public async Task<string> GetUserPhoneNumberAsync(string userId)
        {
            var user = await this.userService.GetUserByIdAsync(userId);
            var phoneNumber = user.PhoneNumber.TrimStart(new char[] { '+', '3', '5', '9' });

            return phoneNumber;
        }

    }
}
