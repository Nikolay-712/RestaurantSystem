namespace RestaurantSystem.Web.Controllers.Contacts
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Contacts;
    using RestaurantSystem.Services.Reservations;
    using RestaurantSystem.Web.Infrastructure;
    using RestaurantSystem.Web.ViewModels.Contacts;
    using RestaurantSystem.Web.ViewModels.Reservations;

    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IContactService contactService;
        private readonly IReservationService reservationService;

        public ContactsController(
            IContactService contactService,
            IReservationService reservationService)
        {
            this.contactService = contactService;
            this.reservationService = reservationService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MessageInputVewModel messageInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(messageInput);
            }

            var sender = ClaimsPrincipalExtensions.Email(this.User);
            await this.contactService.SendMessageAsync(messageInput, sender);

            var message = $"Благодаря за вашето саобщение,ще се свържем с вас на посочения email - {sender}";
            this.TempData["message"] = message;

            return this.Redirect("/");
        }

        public async Task<IActionResult> Reservation()
        {
            var phoneNumber = await this.reservationService
                .GetUserPhoneNumberAsync(ClaimsPrincipalExtensions.Id(this.User));

            if (phoneNumber != null)
            {
                var reservationInput = new ReservationInputViewModel
                {
                    PhoneNumber = phoneNumber,
                    Date = DateTime.UtcNow,
                };

                return this.View(reservationInput);
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Reservation(ReservationInputViewModel reservationInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(reservationInput);
            }

            var result = await this.reservationService
                 .SendReservationAsync(reservationInput, ClaimsPrincipalExtensions.Id(this.User));

            if (!result)
            {
                return this.NotFound();
            }

            var message = "Вашата резервация бевше изпратена успешно.След като бъде обработена ше получите потварждение";
            this.TempData["reservation"] = message;

            return this.Redirect("/");
        }
    }
}
