namespace RestaurantSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Contacts;
    using RestaurantSystem.Web.ViewModels.Administration.Messages;

    public class DashboardController : AdministrationController
    {
        private readonly IContactService contactService;

        public DashboardController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        public IActionResult AllMessages()
        {
            var messages = this.contactService.AllMessages();

            return this.View(messages);
        }

        public IActionResult ReadMessage(string messageId)
        {
            var message = this.contactService
                .GetMessages<AdminMessageViewModel>()
                .FirstOrDefault(x => x.Id == messageId);

            return this.View(message);
        }

        [HttpPost]
        public IActionResult ReadMessage(AdminMessageViewModel adminMessage)
        {
            this.contactService.ReturnАnswer(adminMessage.Id);

            return this.View();
        }
    }
}
