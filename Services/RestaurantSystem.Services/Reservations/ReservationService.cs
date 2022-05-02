﻿namespace RestaurantSystem.Services.Reservations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Models.Reservations;
    using RestaurantSystem.Services.Mapping;
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

            return result;
        }

        public IEnumerable<T> AllResarvations<T>()
        {
            return this.applicationDbContext
                 .Reservations.To<T>();
        }

        public async Task<bool> ChangeReservationStatusAsync(string resarvationId, string status)
        {
            var resarvation = this.applicationDbContext
                .Reservations
                .FirstOrDefault(x => x.Id == resarvationId);

            ReservationStatus parseResult;
            Enum.TryParse<ReservationStatus>(status, out parseResult);

            if (resarvation == null)
            {
                return false;
            }

            resarvation.ReservationStatus = parseResult;

            this.applicationDbContext.Update(resarvation);
            var result = await this.applicationDbContext.SaveChangesAsync();

            return result == 1 ? true : false;
        }

        public async Task<string> GetUserPhoneNumberAsync(string userId)
        {
            var user = await this.userService.GetUserByIdAsync(userId);
            var phoneNumber = user.PhoneNumber.TrimStart(new char[] { '+', '3', '5', '9' });

            return phoneNumber;
        }
    }
}
