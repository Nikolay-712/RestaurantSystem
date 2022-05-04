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

        [HttpPost]
        public async Task<IActionResult> ReadMessage(AllMessagesViewModel replyInput, string messageId)
        {
            var result = await this.contactService.ReplyMessage(replyInput, messageId);

            if (!result)
            {
                return this.NotFound();
            }

            return this.RedirectToAction("AllMessages");
        }
    }
}
