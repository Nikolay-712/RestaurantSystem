namespace RestaurantSystem.Web.Controllers.Users
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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

            this.notificationService.ChangeNotificationStatus(notificationId);

            return this.View(order);
        }

        public IActionResult ReservationDetails(string targetId, string notificationId)
        {
            var reservation = this.reservationService
                .AllResarvations<UserReservationViewModel>()
                .FirstOrDefault(x => x.Id == targetId);

            this.notificationService.ChangeNotificationStatus(notificationId);
            return this.View(reservation);
        }

        public IActionResult MessageDetails(string targetId, string notificationId)
        {
            var message = this.contactService.ReadMessage(targetId);

            this.notificationService.ChangeNotificationStatus(notificationId);
            return this.View(message);
        }
    }
}
