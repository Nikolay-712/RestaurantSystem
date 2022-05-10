namespace RestaurantSystem.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Contacts;
    using RestaurantSystem.Web.ViewModels.Contacts;

    using static RestaurantSystem.Common.GlobalConstants;

    public class DashboardController : AdministrationController
    {
        private readonly IContactService contactService;

        public DashboardController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        public IActionResult AllMessages(int page = 1)
        {
            var messages = this.contactService.AllMessages(page);

            return this.View(messages);
        }

        public IActionResult ReadMessage(string messageId)
        {
            var message = this.contactService.ReadMessage(messageId);

            if (messageId == null || message == null)
            {
                return this.NotFound();
            }

            return this.View(message);
        }

        [HttpPost]
        public async Task<IActionResult> ReadMessage(AppMessageViewModel readMessage)
        {
            var sender = Message.AdminSender;
            var result = await this.contactService
                .ReplyMessageAsync(readMessage.Id, readMessage.ReplyInput.Text, sender);

            if (!result)
            {
                return this.NotFound();
            }

            return this.RedirectToAction("ReadMessage", new { messageId = readMessage.Id });
        }

        public async Task<IActionResult> CloseDiscussion(string messageId)
        {
            var message = this.contactService.GetMessageById(messageId);

            if (message == null)
            {
                return this.NotFound();
            }

            await this.contactService.CloseDiscussionAsync(message);
            return this.RedirectToAction("ReadMessage", new { messageId = message.Id });
        }
    }
}
