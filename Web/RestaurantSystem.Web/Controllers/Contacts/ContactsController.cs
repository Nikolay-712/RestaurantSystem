namespace RestaurantSystem.Web.Controllers.Contacts
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantSystem.Services.Contacts;
    using RestaurantSystem.Web.Infrastructure;
    using RestaurantSystem.Web.ViewModels.Contacts;

    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IContactService contactService;

        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
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
    }
}
