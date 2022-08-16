namespace RestaurantSystem.Web.Controllers.Users
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Data.Models.Orders;
    using RestaurantSystem.Services.Contacts;
    using RestaurantSystem.Services.Notifications;
    using RestaurantSystem.Services.Orders;
    using RestaurantSystem.Services.Reservations;
    using RestaurantSystem.Web.Infrastructure;
    using RestaurantSystem.Web.ViewModels.Reservations;

    [Authorize]
    public class UsersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IReservationService reservationService;
        private readonly INotificationService notificationService;
        private readonly IContactService contactService;

        public UsersController(
            IOrderService orderService,
            IReservationService reservationService,
            INotificationService notificationService,
            IContactService contactService)
        {
            this.orderService = orderService;
            this.reservationService = reservationService;
            this.notificationService = notificationService;
            this.contactService = contactService;
        }

        public IActionResult MyOrders(int page = 1)
        {
            var userId = ClaimsPrincipalExtensions.Id(this.User);
            var orders = this.orderService.GetUserOrders(userId, page);

            return this.View(orders);
        }

        public IActionResult OrderDetails(string targetId, string notificationId)
        {
            var userId = ClaimsPrincipalExtensions.Id(this.User);
            var order = this.orderService
                .GetUserOrders(userId, 1)
                .AllOrders
                .FirstOrDefault(x => x.Id == targetId);

            var status = this.notificationService.ChangeNotificationStatus(notificationId);

            if (order == null || !status)
            {
                return this.NotFound();
            }

            return this.View(order);
        }

        public IActionResult ReservationDetails(string targetId, string notificationId)
        {
            var reservation = this.reservationService
                .AllResarvations<UserReservationViewModel>()
                .FirstOrDefault(x => x.Id == targetId);

            var status = this.notificationService.ChangeNotificationStatus(notificationId);

            if (reservation == null || !status)
            {
                return this.NotFound();
            }

            return this.View(reservation);
        }

        public IActionResult MessageDetails(string targetId, string notificationId)
        {
            var message = this.contactService.ReadMessage(targetId);

            var status = this.notificationService.ChangeNotificationStatus(notificationId);

            if (message == null || !status)
            {
                return this.NotFound();
            }

            return this.View(message);
        }
    }
}
