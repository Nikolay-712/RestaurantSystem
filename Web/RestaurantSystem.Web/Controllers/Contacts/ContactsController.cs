namespace RestaurantSystem.Web.Controllers.Contacts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Contacts;
    using RestaurantSystem.Services.Reservations;
    using RestaurantSystem.Web.Infrastructure;
    using RestaurantSystem.Web.ViewModels.Contacts;
    using RestaurantSystem.Web.ViewModels.Reservations;

    using static RestaurantSystem.Common.GlobalConstants;

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

            var userId = ClaimsPrincipalExtensions.Id(this.User);
            await this.contactService.SendMessageAsync(messageInput, userId);

            var message = Message.SuccessfullySentMessage;
            this.TempData["message"] = message;

            return this.Redirect("/");
        }

        public async Task<IActionResult> Reservation(string restaurantId)
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

            var message = Message.SuccessfullySentReservation;
            this.TempData["reservation"] = message;

            return this.Redirect("/");
        }

        public IActionResult MyMessages()
        {
            var userId = ClaimsPrincipalExtensions.Id(this.User);
            var messages = this.contactService.GetMessages<AppMessageViewModel>()
                .Where(x => x.UserId == userId);

            return this.View(messages);
        }

        public IActionResult ReadMessage(string messageId)
        {
            var userId = ClaimsPrincipalExtensions.Id(this.User);

            var message = this.contactService
                   .GetMessages<AppMessageViewModel>()
                   .Where(x => x.UserId == userId)
                   .FirstOrDefault(x => x.Id == messageId);

            if (messageId == null || message == null)
            {
                return this.NotFound();
            }

            return this.View(message);
        }

        [HttpPost]
        public async Task<IActionResult> ReadMessage(AppMessageViewModel replyMessage)
        {
            var sender = Message.UserSender;

            var result = await this.contactService
                .ReplyMessageAsync(replyMessage.Id, replyMessage.ReplyInput.Text, sender);

            if (!result)
            {
                return this.NotFound();
            }

            return this.RedirectToAction("ReadMessage", new { messageId = replyMessage.Id });
        }
    }
}
