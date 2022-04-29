namespace RestaurantSystem.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

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
            var message = this.contactService.GetMessageAnswers(messageId);

            if (message == null)
            {
                return this.NotFound();
            }

            return this.View(message);
        }

        [HttpPost]
        public async Task<IActionResult> ReadMessage(AdminMessageViewModel adminMessage)
        {
            var result = await this.contactService.ReturnАnswerAsync(adminMessage);
            return result is true ? this.RedirectToAction("AllMessages", "Dashboard") : this.NotFound();
        }
    }
}
